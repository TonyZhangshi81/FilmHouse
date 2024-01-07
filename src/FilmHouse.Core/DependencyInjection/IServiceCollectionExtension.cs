using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using FilmHouse.Core.Services.HttpClients;
using FilmHouse.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace FilmHouse.Core.DependencyInjection;

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
                    .Where(_ => _.GetCustomAttribute<ServiceRegisterAttribute>()?.Lifetime == null || _.GetCustomAttribute<ServiceRegisterAttribute>()!.Lifetime != FilmHouseServiceLifetime.None)
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

    private static void AddService(this IServiceCollection services, FilmHouseServiceLifetime lifetime, Type serviceType, IEnumerable<Type> implementationTypes, bool isLazy)
    {
        foreach (var implementationType in implementationTypes)
        {
            switch (lifetime)
            {
                case FilmHouseServiceLifetime.Scoped:
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
                case FilmHouseServiceLifetime.Singleton:
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
                case FilmHouseServiceLifetime.Transient:
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
                case FilmHouseServiceLifetime.None:
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



    /// <summary>
    /// 根据设定的信息进行设定以生成<see cref="HttpClient"/>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFilmHouseHttpClient(this IServiceCollection services)
    {
        // 为了HttpClient的构建，在这里生成一次ServiceProvider。
        var serviceProvider = services.BuildServiceProvider();
        var httpClientConfigurations = serviceProvider.GetServices<IFilmHouseHttpClientConfiguration>();
        if (httpClientConfigurations == null || !httpClientConfigurations.Any())
        {
            services.AddHttpClient();
            return services;
        }

        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory?.CreateLogger<FilmHouse.Core.Logging.Categories.Core>();
        var jitterier = RandomProvider.GetThreadRandom();

        var doneApi = new HashSet<string>();
        foreach (var httpClientConfig in httpClientConfigurations)
        {
            var fucntionId = $"{httpClientConfig.FunctionId}";
            if (doneApi.Contains(fucntionId))
            {
                // 已经设定好的情况下跳过
                continue;
            }

            doneApi.Add(fucntionId);

            var policyBuilder = Policy<HttpResponseMessage>.Handle<HttpRequestException>();
            httpClientConfig.BuildPolicy(policyBuilder);
            var retryPolicy = policyBuilder.WaitAndRetryAsync(
                httpClientConfig.RetryCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterier.Next(0, 100)),
                onRetry: (message, timeSpan, count, context) =>
                {
                    logger?.LogError(new EventId(0, Guid.NewGuid().ToString("N")), message.Exception, $"webapi execution failed. ({count} times)");
                });

            services.AddHttpClient(ConfigurationPath.Combine(fucntionId), configureClient =>
                    {
                        configureClient.BaseAddress = httpClientConfig.GetApiUri();
                        configureClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
                        {
                            CharSet = System.Text.Encoding.UTF8.WebName,
                        });
                        // 只有在从ajax调用时是必须的，这里省略
                        //configureClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                        httpClientConfig.SetApiKey(configureClient);
                        if (httpClientConfig.Timeout != null)
                        {
                            configureClient.Timeout = httpClientConfig.Timeout.Value;
                        }
                    })
                    .ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        var handler = new HttpClientHandler()
                        {
                            UseProxy = httpClientConfig.UseProxy,
                        };
                        if (httpClientConfig.ProxyAddress != null)
                        {
                            handler.Proxy = new WebProxy(httpClientConfig.ProxyAddress);
                        }
                        // 设定SSL证书即使是自己的证书也能通过
                        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) => true;
                        return handler;
                    })
                    //.AddHttpMessageHandler((serviceProvider) =>
                    //{
                    // 记录输出用的处理程序设定
                    //return serviceProvider.GetRequiredService<HttpMessageLoggingHandler>();
                    //})
                    .AddHttpMessageHandler((serviceProvider) =>
                    {
                        // 当API响应未被返回时，返回专有响应的处理程序设置
                        return serviceProvider.GetRequiredService<HttpMessageErrorHandler>();
                    })
                    // DNS切换时的高速缓存丢弃时间。暂且保持默认的2分钟
                    // .SetHandlerLifetime(TimeSpan.Parse(""))
                    .AddPolicyHandler(retryPolicy);
        }

        return services;
    }
}
