using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Presentation.Web.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.Constants;
using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Core.Presentation.Web.Filters;

[ImplementationRegister(21)]
public class WebApiKeyValidateFilter : IActionFilter, IFilter
{
    private const string ApiKeyRequestHeaderItem = "x-api-key";

    private readonly IConfiguration _configuration;
    private readonly ILogger<Logging.Categories.Web.Api.Authorize> _loggerAuth;
    private readonly ILogger<Logging.Categories.Web.Api.Authorize.Sensitive> _loggerAuthSensitive;

    /// <summary>
    /// <see cref="WebApiKeyValidateFilter"/>
    /// </summary>
    public WebApiKeyValidateFilter(
        IConfiguration configuration,
        ILogger<Logging.Categories.Web.Api.Authorize> loggerAuth,
        ILogger<Logging.Categories.Web.Api.Authorize.Sensitive> loggerAuthSensitive)
    {
        this._configuration = configuration;
        this._loggerAuth = loggerAuth;
        this._loggerAuthSensitive = loggerAuthSensitive;
    }

    /// <summary>
    /// 在批准处理之后立即执行，如果控制器是屏幕辅助的WebAPI，验证是否被赋予了规定的头部。
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="InvalidCredentialException ">在访问api时，如果x-api-key报头的设置是不正确的，则会抛出异常。</exception>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // WebAPI
        var routePath = context.HttpContext.Request.Path.ToString();
        var paths = routePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var request = context.HttpContext.Request;

        if (!paths.Contains("api"))
        {
            this._loggerAuth.LogTrace($"Skip API key authentication because the URL path does not include /API.");
            return;
        }

        // 对于API专用项目的请求，验证x-api-key是否正确。
        if (this.TryGetApiKey(request, out var webApiKey))
        {
            var validApiKey = Guard.GetNotNull(this._configuration.GetValue<WebApiKeyVO>($"WebApi:Apikey:Inbound"), $"WebApi:Apikey:Inbound");

            if (webApiKey == validApiKey)
            {
                this._loggerAuth.LogTrace($"API key authentication was successful.");
                return;
            }
            else
            {
                // API键比较的结果确定不一致
                throw new WebApiInvalidResonException($"The value of x-api-key in the HTTP header information and the API key as a setting value did not match.");
            }
        }
        else
        {
            // 将标题信息作为Trace日志输出
            this._loggerAuthSensitive.LogError("{httpheader}", request.Headers.ToJsonString());
            throw new WebApiInvalidResonException($"The x-api-key value could not be obtained from HTTP header information of WebAPI requests.");
        }
    }

    private bool TryGetApiKey(HttpRequest self, out WebApiKeyVO webApiKey)
    {
        webApiKey = null;
        if (self.Headers == null || !self.Headers.ContainsKey(HttpContextItemNames.ApiKeyRequestHeaderItem))
        {
            return false;
        }
        webApiKey = new(self.Headers[HttpContextItemNames.ApiKeyRequestHeaderItem]);
        return true;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}
