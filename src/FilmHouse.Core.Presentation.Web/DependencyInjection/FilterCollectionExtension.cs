using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Core.Presentation.Web.DependencyInjection;

/// <summary>
/// 管理与DI相关的FilterCollection扩展方法的类。
/// </summary>
public static class FilterCollectionExtension
{
    /// <summary>
    /// 注册自定义Filter。
    /// </summary>
    /// <param name="filters"></param>
    /// <param name="services"></param>
    public static void AddFilters(this FilterCollection filters, IServiceCollection services)
    {
        // 注册顺序通过implementationregisterattribute.priority进行指定
        foreach (var filterType in services.Where(_ => _.ServiceType == typeof(IFilter) && _.ImplementationType != null).Select(_ => _.ImplementationType).Cast<Type>())
        {
            filters.Add(filterType);
        }
    }
}
