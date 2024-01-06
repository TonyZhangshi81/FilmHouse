#nullable enable
namespace FilmHouse.Core.Services.HttpClients;

/// <summary>
/// 发送到WebAPI的请求数据实例。
/// </summary>
public class HttpJsonRequestMessage : HttpRequestMessage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    public HttpJsonRequestMessage(JsonStringContent? content)
        : base()
    {
        this.Content = content;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <param name="requestUri"></param>
    public HttpJsonRequestMessage(HttpMethod method, string? requestUri)
        : base(method, requestUri)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <param name="method"></param>
    /// <param name="requestUri"></param>
    public HttpJsonRequestMessage(JsonStringContent? content, HttpMethod method, string? requestUri)
        : base(method, requestUri)
    {
        this.Content = content;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <param name="requestUri"></param>
    public HttpJsonRequestMessage(HttpMethod method, Uri? requestUri)
        : base(method, requestUri)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <param name="method"></param>
    /// <param name="requestUri"></param>
    public HttpJsonRequestMessage(JsonStringContent? content, HttpMethod method, Uri? requestUri)
        : base(method, requestUri)
    {
        this.Content = content;
    }
}
