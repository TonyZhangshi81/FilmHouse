#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.DependencyInjection;

/// <summary>
/// 对在DI注册的服务的类型赋予。
/// </summary>
/// <remarks>
/// 将该属性赋予接口时，将接口注册为服务，将实现接口的类注册为实现类。
/// 如果将该属性添加到类中，则将类本身注册为服务和实现类。
/// </remarks>
[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public class ServiceRegisterAttribute : Attribute
{
    /// <summary>
    /// <see cref="ServiceRegisterAttribute"/>的新实例。
    /// </summary>
    /// <param name="lifetime">这项服务的有效范围</param>
    /// <param name="serviceType">指定被赋予了这个属性的类型以外的服务型登记的情况。</param>
    /// <param name="isLazy">用于延迟执行<see cref="Lazy{t}" />是否作为型的实例</param>
    public ServiceRegisterAttribute(FilmHouseServiceLifetime lifetime, Type? serviceType = null, bool isLazy = false)
    {
        this.Lifetime = lifetime;
        this.ServiceType = serviceType;
        this.IsLazy = isLazy;
    }

    /// <summary>
    /// 取得这个服务的有效范围。
    /// </summary>
    public FilmHouseServiceLifetime Lifetime { get; }

    /// <summary>
    /// 取得作为服务登记的类型。如果为空，则使用赋予该属性的类型。
    /// </summary>
    public Type? ServiceType { get; }

    /// <summary>
    /// 作为服务实体<see cref="Lazy{t}" />是否用保鲜膜包裹。
    /// </summary>
    public bool IsLazy { get; }
}
