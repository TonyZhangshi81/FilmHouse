using System.Diagnostics.CodeAnalysis;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Presentation.Web.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilmHouse.Core.Presentation.Web.Filters
{
    [ImplementationRegister(20)]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LogonFilterAttribute : Attribute, IAsyncActionFilter, IFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectResult("/Mine/Index");
            }
            await next.Invoke();
        }
    }
}
