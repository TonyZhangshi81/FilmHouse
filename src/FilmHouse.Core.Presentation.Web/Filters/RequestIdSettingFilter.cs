using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Presentation.Web.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FilmHouse.Core.Presentation.Web.Filters
{
    /// <summary>
    /// 成为请求处理中的统一ID，用于注册请求ID的类。
    /// </summary>
    /// <remarks>
    /// 由于在PageModel和Controller的构造器之前有必要执行,所以将实现<see cref="IResourceFilter"/>。
    /// </remarks>
    [ImplementationRegister(10)]
    public class RequestIdSettingFilter : IAsyncResourceFilter, IFilter
    {
        private readonly ICurrentRequestId _requestIdAccessor;

        /// <summary>
        /// <see cref="RequestIdSettingFilter"/>的新实例。
        /// </summary>
        /// <param name="requestIdAccessor">用于注册请求ID的类</param>
        public RequestIdSettingFilter(ICurrentRequestId requestIdAccessor)
        {
            this._requestIdAccessor = requestIdAccessor;
        }

        /// <summary>
        /// 暂停的缺省值
        /// </summary>
        private static readonly RequestIdVO EmptyRequestId = new RequestIdVO(Guid.Empty);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var requestId = EmptyRequestId;

            if (context.ActionDescriptor is CompiledPageActionDescriptor pageDescriptor)
            {
                // 画面
                requestId = new(Guid.NewGuid());
            }

            this._requestIdAccessor.Set(requestId);
            await next.Invoke();
        }
    }
}
