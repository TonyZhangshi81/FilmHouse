using FilmHouse.Core.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilmHouse.Core.Presentation.Web.DependencyInjection
{
    /// <summary>
    /// 页面过滤器和动作过滤器用于注册服务的界面。
    /// </summary>
    [ServiceRegister(FilmHouseServiceLifetime.Transient)]
    public interface IFilter : IFilterMetadata
    {
    }
}
