using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Core.DependencyInjection
{
    /// <summary>
    /// <see cref="IServiceCollection"/>对DB提供相关的扩展方法。
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// <see cref="IServiceCollection"/>与此相对，在应用程序中注册成为注册对象的服务。
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies">成为搜索范围的组合件的收集</param>
        /// <returns>为了实现方法链<paramref name="services" />返还。</returns>
        public static IServiceCollection AddLocalService(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var serviceInterfaceTypes = new HashSet<Type>();

            foreach (var assembly in assemblies)
            {
                serviceInterfaceTypes.UnionWith(assembly.GetTypes().Where(_ => _.IsInterface && _.GetCustomAttribute<ServiceRegisterAttribute>() != null));

                // 接口实现类的服务注册
                services.AddInterfaceServices(assembly, serviceInterfaceTypes);

                // 服务登记
                services.AddClassServices(assembly);
            }
            return services;
        }

        private static void AddInterfaceServices(this IServiceCollection services, Assembly targetAssembly, IEnumerable<Type> serviceInterfaceTypes)
        {
            // 接口实现类的服务注册
            foreach (var serviceInterfaceType in serviceInterfaceTypes)
            {
                var attr = serviceInterfaceType.GetCustomAttribute<ServiceRegisterAttribute>();
                if (attr == null)
                {
                    continue;
                }
                var lifetime = attr.Lifetime;
                var serviceType = attr.ServiceType ?? serviceInterfaceType;
                var isLazy = attr.IsLazy;
                if (serviceType.IsGenericType)
                {
                    // 如果正在实现泛型接口
                    var implementationTypes = targetAssembly.GetTypes()
                        .Where(_ => _.IsClass && !_.IsAbstract && (_.IsPublic || _.IsNestedPublic))
                        .Where(_ => _.GetInterfaces().Any(type => type.IsGenericType && type.GetGenericTypeDefinition() == serviceType))
                        .Where(_ => _.GetCustomAttribute<ImplementationRegisterAttribute>()?.Enabled ?? true)
                        .OrderBy(_ => _.GetCustomAttribute<ImplementationRegisterAttribute>()?.Priority ?? int.MaxValue);
                    foreach (var implementationType in implementationTypes)
                    {
                        var interfaceTypes = implementationType.GetInterfaces().Where(_ => _.IsGenericType && _.GetGenericTypeDefinition() == serviceType);
                        interfaceTypes.ToList().ForEach(_ => services.AddService(lifetime, _, new[] { implementationType }, isLazy));
                    }
                }
                else
                {
                    // 如果你正在实现非泛型接口，
                    var implementationTypes = targetAssembly.GetTypes()
                        .Where(_ => _.IsClass && !_.IsAbstract && serviceType.IsAssignableFrom(_) && (_.IsPublic || _.IsNestedPublic))
                        .Where(_ => _.GetCustomAttribute<ImplementationRegisterAttribute>()?.Enabled ?? true)
                        .Where(_ => _.GetCustomAttribute<ServiceRegisterAttribute>()?.Lifetime == null || _.GetCustomAttribute<ServiceRegisterAttribute>()!.Lifetime != SelfServiceLifetime.None)
                        .OrderBy(_ => _.GetCustomAttribute<ImplementationRegisterAttribute>()?.Priority ?? int.MaxValue);
                    services.AddService(lifetime, serviceType, implementationTypes, isLazy);
                }
            }
        }

        private static void AddClassServices(this IServiceCollection services, Assembly targetAssembly)
        {
            var serviceClassTypes = targetAssembly.GetTypes().Where(_ => _.IsClass && !_.IsAbstract && (_.IsPublic || _.IsNestedPublic) && _.GetCustomAttribute<ServiceRegisterAttribute>() != null);
            foreach (var classType in serviceClassTypes)
            {
                var attr = classType.GetCustomAttribute<ServiceRegisterAttribute>();
                if (attr == null)
                {
                    continue;
                }
                var lifetime = attr.Lifetime;
                var serviceType = attr.ServiceType ?? classType;
                var isLazy = attr.IsLazy;
                services.AddService(lifetime, serviceType, new[] { classType }, isLazy);
            }
        }

        private static void AddService(this IServiceCollection services, SelfServiceLifetime lifetime, Type serviceType, IEnumerable<Type> implementationTypes, bool isLazy)
        {
            foreach (var implementationType in implementationTypes)
            {
                switch (lifetime)
                {
                    case SelfServiceLifetime.Scoped:
                        services.AddScoped(serviceType, implementationType);
                        if (isLazy)
                        {
                            // 为了延迟执行，也登记实体类型的服务
                            services.AddScoped(implementationType, implementationType);
                            services.AddScoped(
                                typeof(Lazy<>).MakeGenericType(serviceType),
                                _ => CreateLazyImplementation(_, serviceType, implementationType));
                        }
                        break;
                    case SelfServiceLifetime.Singleton:
                        services.AddSingleton(serviceType, implementationType);
                        if (isLazy)
                        {
                            // 为了延迟执行，也登记实体类型的服务
                            services.AddSingleton(implementationType, implementationType);
                            services.AddSingleton(
                                typeof(Lazy<>).MakeGenericType(serviceType),
                                _ => CreateLazyImplementation(_, serviceType, implementationType));
                        }
                        break;
                    case SelfServiceLifetime.Transient:
                        services.AddTransient(serviceType, implementationType);
                        if (isLazy)
                        {
                            // 为了延迟执行，也登记实体类型的服务
                            services.AddTransient(implementationType, implementationType);
                            services.AddTransient(
                                typeof(Lazy<>).MakeGenericType(serviceType),
                                _ => CreateLazyImplementation(_, serviceType, implementationType));
                        }
                        break;
                    case SelfServiceLifetime.None:
                        break;
                    default:
                        throw new ArgumentException("得到的自变量并不是有效的值。", nameof(lifetime));
                };
            }
            return;
        }

        /// <summary>
        /// <see cref="ServiceProviderServiceExtensions.GetRequiredService"/>的方法信息。
        /// </summary>
        private static readonly MethodInfo ServiceProviderServiceExtensionsGetRequiredService = typeof(ServiceProviderServiceExtensions)
            .GetMethod(
                name: nameof(ServiceProviderServiceExtensions.GetRequiredService),
                genericParameterCount: 1,
                bindingAttr: BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public,
                binder: null,
                types: new[] { typeof(IServiceProvider) },
                modifiers: null)!;

        /// <summary>
        /// 从DI <see cref="Lazy {t}" />的实例生成处理。
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <returns></returns>
        private static object CreateLazyImplementation(IServiceProvider serviceProvider, Type serviceType, Type implementationType)
        {
            var interfaceTypes = implementationType.GetInterfaces()
                .Where(_ => _ != serviceType && serviceType.IsAssignableFrom(_))
                .Select(_ => new { InterfaceType = _, Length = _.GetInterfaces().Length });
            var interfaceType = interfaceTypes.OrderByDescending(_ => _.Length).Select(_ => _.InterfaceType).FirstOrDefault();
            var lazyType = typeof(Lazier<,>).MakeGenericType(serviceType, interfaceType ?? serviceType);
            var ctorType = typeof(Func<>).MakeGenericType(interfaceType ?? serviceType);

            // 非专利型的生成
            var mi = ServiceProviderServiceExtensionsGetRequiredService.MakeGenericMethod(implementationType);

            // 通过来自DI的类型指定生成实例
            var expression = Expression.Call(mi!, Expression.Constant(serviceProvider));

            // 指定Func<T>作为参数，返回Lazy<T>类型的实例
            var result = Activator.CreateInstance(lazyType, Expression.Lambda(ctorType, expression).Compile())!;
            return result;
        }
    }
}
