using System.Net.Sockets;
using FilmHouse.Core.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IsiFilmHouse.Core.Services.HttpClients;

/// <summary>
/// 当API请求对象由于某种原因没有返回响应时，返回既定响应的handler类。
/// </summary>
[ServiceRegister(FilmHouseServiceLifetime.Transient)]
public class HttpMessageErrorHandler : DelegatingHandler
{
    private readonly ILogger<FilmHouse.Core.Logging.Categories.Core.Error> _errorLogger;

    /// <summary>
    /// 
    /// </summary>
    public HttpMessageErrorHandler(ILogger<FilmHouse.Core.Logging.Categories.Core.Error> errorLogger)
    {
        this._errorLogger = errorLogger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException e) when (e.InnerException is SocketException)
        {
            // API请求目的地服务器停止、网络断开等
            return this.GenerateResponse(request, System.Net.HttpStatusCode.ServiceUnavailable, "There was no response from the API request destination.");
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // API请求超时
            return this.GenerateResponse(request, System.Net.HttpStatusCode.ServiceUnavailable, "There was no response from the API request destination.");
        }
    }

    /// <summary>
    /// 将API请求时发生的错误内容作为日志输出，生成基础独自返回的响应。
    /// </summary>
    /// <returns></returns>
    private HttpResponseMessage GenerateResponse(HttpRequestMessage request, System.Net.HttpStatusCode statusCode, string errorLogMessage)
    {
        this._errorLogger.LogError(errorLogMessage);

        HttpResponseMessage errorResponse = new HttpResponseMessage();
        errorResponse.StatusCode = statusCode;
        errorResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        return errorResponse;
    }
}
