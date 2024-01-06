#nullable enable
using System.Net.Http.Headers;

namespace FilmHouse.Core.Services.HttpClients;

/// <summary>
/// <see cref="HttpClient"/>
/// </summary>
public partial interface IFilmHouseHttpClient
{
    /// <summary>
    /// 获取需要和每个请求一起发送的标题。
    /// </summary>
    HttpRequestHeaders DefaultRequestHeaders { get; }

    /// <summary>
    /// 获取或设置你发送请求时使用的互联网资源统一资源地址(URI)的基本地址。
    /// </summary>
    Uri? BaseAddress { get; set; }
    /// <summary>
    /// 获取或设置可存储在缓冲器中的最大字节来读取响应内容。
    /// </summary>
    long MaxResponseContentBufferSize { get; set; }
    /// <summary>
    /// 获取或设置等待时间，直到请求超时。
    /// </summary>
    TimeSpan Timeout { get; set; }

    /// <summary>
    /// 取消这个实例中保留中的所有要求。
    /// </summary>
    void CancelPendingRequests();
    /// <summary>
    /// 向指定的URI发送DELETE请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> DeleteAsync(Uri? requestUri);
    /// <summary>
    /// 向指定的URI发送DELETE请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> DeleteAsync(string? requestUri);
    /// <summary>
    /// 使用取消令牌将删除请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> DeleteAsync(string? requestUri, CancellationToken cancellationToken);
    /// <summary>
    /// 使用取消令牌将删除请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> DeleteAsync(Uri? requestUri, CancellationToken cancellationToken);

    /// <summary>
    /// 将GET请求作为异步操作发送到指定的URI。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> GetAsync(string? requestUri);
    /// <summary>
    /// 将GET请求作为异步操作发送到指定的URI。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> GetAsync(Uri? requestUri);
    /// <summary>
    /// 使用HTTP完成选项将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="completionOption">表示视为操作完成的定时的HTTP完成可选值。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> GetAsync(string? requestUri, HttpCompletionOption completionOption);
    /// <summary>
    /// 使用取消令牌将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> GetAsync(string? requestUri, CancellationToken cancellationToken);
    /// <summary>
    /// 使用HTTP完成选项将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="completionOption">表示视为操作完成的定时的HTTP完成可选值。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> GetAsync(Uri? requestUri, HttpCompletionOption completionOption);
    /// <summary>
    /// 使用取消令牌将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> GetAsync(Uri? requestUri, CancellationToken cancellationToken);
    /// <summary>
    /// 使用取消令牌和HTTP完成选项将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="completionOption">表示视为操作完成的定时的HTTP完成可选值。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> GetAsync(Uri? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken);
    /// <summary>
    /// 使用取消令牌和HTTP完成选项将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="completionOption">表示视为操作完成的定时的HTTP完成可选值。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> GetAsync(string? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken);

    /// <summary>
    /// 将GET请求发送到指定URI，通过异步操作将响应主体作为字节阵列返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<byte[]> GetByteArrayAsync(string? requestUri);
    /// <summary>
    /// 将GET请求发送到指定URI，通过异步操作将响应主体作为字节阵列返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<byte[]> GetByteArrayAsync(Uri? requestUri);
    /// <summary>
    /// 将GET请求发送到指定URI，通过异步操作将响应主体作为字节阵列返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<byte[]> GetByteArrayAsync(string? requestUri, CancellationToken cancellationToken);
    /// <summary>
    /// 将GET请求发送到指定URI，通过异步操作将响应主体作为字节阵列返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<byte[]> GetByteArrayAsync(Uri? requestUri, CancellationToken cancellationToken);

    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作作为流返回响应主体。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<Stream> GetStreamAsync(string? requestUri);
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作作为流返回响应主体。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<Stream> GetStreamAsync(Uri? requestUri);
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作作为流返回响应主体。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<Stream> GetStreamAsync(string? requestUri, CancellationToken cancellationToken);
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作作为流返回响应主体。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<Stream> GetStreamAsync(Uri? requestUri, CancellationToken cancellationToken);

    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作将响应主体作为字符串返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<string> GetStringAsync(Uri? requestUri);
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作将响应主体作为字符串返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<string> GetStringAsync(string? requestUri);
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作将响应主体作为字符串返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<string> GetStringAsync(string? requestUri, CancellationToken cancellationToken);
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作将响应主体作为字符串返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<string> GetStringAsync(Uri? requestUri, CancellationToken cancellationToken);

    /// <summary>
    /// 向指定的URI发送PATCH请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PatchAsync(string? requestUri, JsonStringContent content);
    /// <summary>
    /// 向指定的URI发送PATCH请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PatchAsync(Uri? requestUri, JsonStringContent content);
    /// <summary>
    /// 向指定的URI发送带有取消令牌的PATCH请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PatchAsync(string? requestUri, JsonStringContent content, CancellationToken cancellationToken);
    /// <summary>
    /// 向指定的URI发送带有取消令牌的PATCH请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PatchAsync(Uri? requestUri, JsonStringContent content, CancellationToken cancellationToken);

    /// <summary>
    /// 向指定的URI发送POST请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PostAsync(string? requestUri, JsonStringContent content);
    /// <summary>
    /// 向指定的URI发送POST请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PostAsync(Uri? requestUri, JsonStringContent content);
    /// <summary>
    /// 将POST请求作为异步操作发送到指定的URI，并带有取消令牌。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PostAsync(string? requestUri, JsonStringContent content, CancellationToken cancellationToken);
    /// <summary>
    /// 将POST请求作为异步操作发送到指定的URI，并带有取消令牌。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PostAsync(Uri? requestUri, JsonStringContent content, CancellationToken cancellationToken);

    /// <summary>
    /// 向指定的URI发送PUT请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PutAsync(string? requestUri, JsonStringContent content);
    /// <summary>
    /// 向指定的URI发送PUT请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PutAsync(Uri? requestUri, JsonStringContent content);
    /// <summary>
    /// 向指定的URI发送带有取消令牌的PUT请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PutAsync(string? requestUri, JsonStringContent content, CancellationToken cancellationToken);
    /// <summary>
    /// 向指定的URI发送带有取消令牌的PUT请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> PutAsync(Uri? requestUri, JsonStringContent content, CancellationToken cancellationToken);

    /// <summary>
    /// 使用指定的请求发送HTTP请求。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <returns>HTTP响应消息</returns>
    HttpResponseMessage Send(HttpJsonRequestMessage request);
    /// <summary>
    /// 使用指定的请求发送HTTP请求。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="completionOption">指定当操作完成时(在响应变为可用状态之后或在响应内容被读取之后)的列举值之一。</param>
    /// <returns>HTTP响应消息</returns>
    HttpResponseMessage Send(HttpJsonRequestMessage request, HttpCompletionOption completionOption);
    /// <summary>
    /// 使用指定的请求发送HTTP请求。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>HTTP响应消息</returns>
    HttpResponseMessage Send(HttpJsonRequestMessage request, CancellationToken cancellationToken);
    /// <summary>
    /// 使用指定的请求发送HTTP请求。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="completionOption">指定当操作完成时(在响应变为可用状态之后或在响应内容被读取之后)的列举值之一。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>HTTP响应消息</returns>
    HttpResponseMessage Send(HttpJsonRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken);

    /// <summary>
    /// 发送HTTP请求作为异步操作。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> SendAsync(HttpJsonRequestMessage request);
    /// <summary>
    /// 发送HTTP请求作为异步操作。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="completionOption">指定当操作完成时(在响应变为可用状态之后或在响应内容被读取之后)的列举值之一。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> SendAsync(HttpJsonRequestMessage request, HttpCompletionOption completionOption);
    /// <summary>
    /// 发送HTTP请求作为异步操作。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> SendAsync(HttpJsonRequestMessage request, CancellationToken cancellationToken);
    /// <summary>
    /// 发送HTTP请求作为异步操作。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="completionOption">指定当操作完成时(在响应变为可用状态之后或在响应内容被读取之后)的列举值之一。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    Task<HttpResponseMessage> SendAsync(HttpJsonRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken);
}
