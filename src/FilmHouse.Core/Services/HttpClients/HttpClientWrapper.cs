#nullable enable
using System.Net.Http.Headers;
using FilmHouse.Core.Utils;

namespace FilmHouse.Core.Services.HttpClients;

/// <summary>
/// <see cref="HttpClient"/>
/// </summary>
public partial class HttpClientWrapper : IFilmHouseHttpClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// <see cref="HttpClientWrapper"/>
    /// </summary>
    /// <param name="httpClient"></param>
    public HttpClientWrapper(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    /// <summary>
    /// 获取需要和每个请求一起发送的标题。
    /// </summary>
    public HttpRequestHeaders DefaultRequestHeaders => this._httpClient.DefaultRequestHeaders;

    /// <summary>
    /// 获取或设置你发送请求时使用的互联网资源统一资源地址(URI)的基本地址。
    /// </summary>
    public Uri? BaseAddress { get => this._httpClient.BaseAddress; set => this._httpClient.BaseAddress = value; }
    /// <summary>
    /// 获取或设置可存储在缓冲器中的最大字节来读取响应内容。
    /// </summary>
    public long MaxResponseContentBufferSize { get => this._httpClient.MaxResponseContentBufferSize; set => this._httpClient.MaxResponseContentBufferSize = value; }
    /// <summary>
    /// 获取或设置等待时间，直到请求超时。
    /// </summary>
    public TimeSpan Timeout { get => this._httpClient.Timeout; set => this._httpClient.Timeout = value; }

    /// <summary>
    /// 取消这个实例中保留中的所有要求。
    /// </summary>
    public void CancelPendingRequests() => this._httpClient.CancelPendingRequests();
    /// <summary>
    /// 向指定的URI发送DELETE请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> DeleteAsync(Uri? requestUri)
    {
        try
        {
            return await this._httpClient.DeleteAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 向指定的URI发送DELETE请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> DeleteAsync(string? requestUri)
    {
        try
        {
            return await this._httpClient.DeleteAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用取消令牌将删除请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> DeleteAsync(string? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.DeleteAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用取消令牌将删除请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> DeleteAsync(Uri? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.DeleteAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }

    /// <summary>
    /// 将GET请求作为异步操作发送到指定的URI。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> GetAsync(string? requestUri)
    {
        try
        {
            return await this._httpClient.GetAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);

        }
    }
    /// <summary>
    /// 将GET请求作为异步操作发送到指定的URI。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> GetAsync(Uri? requestUri)
    {
        try
        {
            return await this._httpClient.GetAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用HTTP完成选项将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="completionOption">表示视为操作完成的定时的HTTP完成可选值。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> GetAsync(string? requestUri, HttpCompletionOption completionOption)
    {
        try
        {
            return await this._httpClient.GetAsync(requestUri, completionOption);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用取消令牌将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> GetAsync(string? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用HTTP完成选项将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="completionOption">表示视为操作完成的定时的HTTP完成可选值。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> GetAsync(Uri? requestUri, HttpCompletionOption completionOption)
    {
        try
        {
            return await this._httpClient.GetAsync(requestUri, completionOption);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用取消令牌将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> GetAsync(Uri? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用取消令牌和HTTP完成选项将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="completionOption">表示视为操作完成的定时的HTTP完成可选值。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> GetAsync(Uri? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetAsync(requestUri, completionOption, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用取消令牌和HTTP完成选项将GET请求发送到指定的Uri作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="completionOption">表示视为操作完成的定时的HTTP完成可选值。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> GetAsync(string? requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetAsync(requestUri, completionOption, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }

    /// <summary>
    /// 将GET请求发送到指定URI，通过异步操作将响应主体作为字节阵列返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<byte[]> GetByteArrayAsync(string? requestUri)
    {
        try
        {
            return await this._httpClient.GetByteArrayAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
        }
    }
    /// <summary>
    /// 将GET请求发送到指定URI，通过异步操作将响应主体作为字节阵列返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<byte[]> GetByteArrayAsync(Uri? requestUri)
    {
        try
        {
            return await this._httpClient.GetByteArrayAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
        }
    }
    /// <summary>
    /// 将GET请求发送到指定URI，通过异步操作将响应主体作为字节阵列返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<byte[]> GetByteArrayAsync(string? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetByteArrayAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
        }
    }
    /// <summary>
    /// 将GET请求发送到指定URI，通过异步操作将响应主体作为字节阵列返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<byte[]> GetByteArrayAsync(Uri? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetByteArrayAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
        }
    }

    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作作为流返回响应主体。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<Stream> GetStreamAsync(string? requestUri)
    {
        try
        {
            return await this._httpClient.GetStreamAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).Content.ReadAsStreamAsync().GetAwaiter().GetResult();
        }
    }
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作作为流返回响应主体。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<Stream> GetStreamAsync(Uri? requestUri)
    {
        try
        {
            return await this._httpClient.GetStreamAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).Content.ReadAsStreamAsync().GetAwaiter().GetResult();
        }
    }
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作作为流返回响应主体。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<Stream> GetStreamAsync(string? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetStreamAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).Content.ReadAsStreamAsync().GetAwaiter().GetResult();
        }
    }
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作作为流返回响应主体。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<Stream> GetStreamAsync(Uri? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetStreamAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).Content.ReadAsStreamAsync().GetAwaiter().GetResult();
        }
    }

    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作将响应主体作为字符串返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<string> GetStringAsync(Uri? requestUri)
    {
        try
        {
            return await this._httpClient.GetStringAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).ToJsonString();
        }
    }
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作将响应主体作为字符串返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<string> GetStringAsync(string? requestUri)
    {
        try
        {
            return await this._httpClient.GetStringAsync(requestUri);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).ToJsonString();
        }
    }
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作将响应主体作为字符串返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<string> GetStringAsync(string? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetStringAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).ToJsonString();
        }
    }
    /// <summary>
    /// 向指定URI发送GET请求，通过异步操作将响应主体作为字符串返回。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<string> GetStringAsync(Uri? requestUri, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.GetStringAsync(requestUri, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable).ToJsonString();
        }
    }

    /// <summary>
    /// 向指定的URI发送PATCH请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PatchAsync(string? requestUri, JsonStringContent content)
    {
        try
        {
            return await this._httpClient.PatchAsync(requestUri, content);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 向指定的URI发送PATCH请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PatchAsync(Uri? requestUri, JsonStringContent content)
    {
        try
        {
            return await this._httpClient.PatchAsync(requestUri, content);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 向指定的URI发送带有取消令牌的PATCH请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PatchAsync(string? requestUri, JsonStringContent content, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.PatchAsync(requestUri, content, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 向指定的URI发送带有取消令牌的PATCH请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PatchAsync(Uri? requestUri, JsonStringContent content, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.PatchAsync(requestUri, content, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }

    /// <summary>
    /// 向指定的URI发送POST请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PostAsync(string? requestUri, JsonStringContent content)
    {
        try
        {
            return await this._httpClient.PostAsync(requestUri, content);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 向指定的URI发送POST请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PostAsync(Uri? requestUri, JsonStringContent content)
    {
        try
        {
            return await this._httpClient.PostAsync(requestUri, content);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }

    /// <summary>
    /// 将POST请求作为异步操作发送到指定的URI，并带有取消令牌。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PostAsync(string? requestUri, JsonStringContent content, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.PostAsync(requestUri, content, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 将POST请求作为异步操作发送到指定的URI，并带有取消令牌。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PostAsync(Uri? requestUri, JsonStringContent content, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.PostAsync(requestUri, content, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }

    /// <summary>
    /// 向指定的URI发送PUT请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PutAsync(string? requestUri, JsonStringContent content)
    {
        try
        {
            return await this._httpClient.PutAsync(requestUri, content);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 向指定的URI发送PUT请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PutAsync(Uri? requestUri, JsonStringContent content)
    {
        try
        {
            return await this._httpClient.PutAsync(requestUri, content);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 向指定的URI发送带有取消令牌的PUT请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PutAsync(string? requestUri, JsonStringContent content, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.PutAsync(requestUri, content, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 向指定的URI发送带有取消令牌的PUT请求作为异步操作。
    /// </summary>
    /// <param name="requestUri">请求的发送目的地URI。</param>
    /// <param name="content">发送到服务器的HTTP请求的内容。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> PutAsync(Uri? requestUri, JsonStringContent content, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.PutAsync(requestUri, content, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }

    /// <summary>
    /// 使用指定的请求发送HTTP请求。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <returns>HTTP响应消息</returns>
    public HttpResponseMessage Send(HttpJsonRequestMessage request)
    {
        try
        {
            return this._httpClient.Send(request);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用指定的请求发送HTTP请求。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="completionOption">指定当操作完成时(在响应变为可用状态之后或在响应内容被读取之后)的列举值之一。</param>
    /// <returns>HTTP响应消息</returns>
    public HttpResponseMessage Send(HttpJsonRequestMessage request, HttpCompletionOption completionOption)
    {
        try
        {
            return this._httpClient.Send(request, completionOption);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用指定的请求发送HTTP请求。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>HTTP响应消息</returns>
    public HttpResponseMessage Send(HttpJsonRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            return this._httpClient.Send(request, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 使用指定的请求发送HTTP请求。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="completionOption">指定当操作完成时(在响应变为可用状态之后或在响应内容被读取之后)的列举值之一。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>HTTP响应消息</returns>
    public HttpResponseMessage Send(HttpJsonRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        try
        {
            return this._httpClient.Send(request, completionOption, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }

    /// <summary>
    /// 发送HTTP请求作为异步操作。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> SendAsync(HttpJsonRequestMessage request)
    {
        try
        {
            return await this._httpClient.SendAsync(request);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 发送HTTP请求作为异步操作。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="completionOption">指定当操作完成时(在响应变为可用状态之后或在响应内容被读取之后)的列举值之一。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> SendAsync(HttpJsonRequestMessage request, HttpCompletionOption completionOption)
    {
        try
        {
            return await this._httpClient.SendAsync(request, completionOption);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 发送HTTP请求作为异步操作。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> SendAsync(HttpJsonRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.SendAsync(request, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }
    /// <summary>
    /// 发送HTTP请求作为异步操作。
    /// </summary>
    /// <param name="request">发送的HTTP请求消息。</param>
    /// <param name="completionOption">指定当操作完成时(在响应变为可用状态之后或在响应内容被读取之后)的列举值之一。</param>
    /// <param name="cancellationToken">取消令牌可在其它对象或线程中使用以接收取消通知。</param>
    /// <returns>表示异步操作的任务对象。</returns>
    public async Task<HttpResponseMessage> SendAsync(HttpJsonRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        try
        {
            return await this._httpClient.SendAsync(request, completionOption, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // API请求超时
            return this.GenerateResponse(System.Net.HttpStatusCode.ServiceUnavailable);
        }
    }

    /// <summary>
    /// 当API请求时发生错误时，生成基础独自返回的响应。
    /// </summary>
    /// <returns></returns>
    private HttpResponseMessage GenerateResponse(System.Net.HttpStatusCode statusCode)
    {
        // 响应生成
        var errorResponse = new HttpResponseMessage();
        errorResponse.StatusCode = statusCode;
        errorResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return errorResponse;
    }
}
