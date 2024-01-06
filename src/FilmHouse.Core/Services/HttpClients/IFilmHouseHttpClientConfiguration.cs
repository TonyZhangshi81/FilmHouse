using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.ValueObjects;
using Polly;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FilmHouse.Core.Services.HttpClients;

/// <summary>
/// 设置生成<see cref="IFilmHouseHttpClientFactory"/>到<see cref="HttpClient"/>的实例。
/// </summary>
[ServiceRegister(FilmHouseServiceLifetime.Singleton)]
public interface IFilmHouseHttpClientConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    string FunctionId { get; }

    /// <summary>
    /// 获取调用目标WebAPI时指定的API密钥。
    /// </summary>
    WebApiKeyVO ApiKey { get; }

    /// <summary>
    /// 设置调用WebAPI时的验证信息。
    /// </summary>
    void SetApiKey(HttpClient httpClient);

    /// <summary>
    /// 获取目标WebAPI调用超时的时间。
    /// </summary>
    TimeSpan? Timeout { get; }

    /// <summary>
    /// 处理失败时，取得进行重试时的重试执行次数。
    /// </summary>
    int RetryCount { get; }

    /// <summary>
    /// 获取被定义的代理使用与否。
    /// </summary>
    /// <remarks>
    /// 在没有设定的情况下，不使用代理服务器。
    /// </remarks>
    bool UseProxy { get; }

    /// <summary>
    /// 获取定义的代理地址。
    /// </summary>
    /// <remarks>
    /// <see cref="UseProxy"/>为true时才获取。
    /// </remarks>
    string ProxyAddress { get; }

    /// <summary>
    /// 获取URI。
    /// </summary>
    /// <returns></returns>
    Uri GetApiUri();

    /// <summary>
    /// <see cref="HttpClient"/>的策略设定。
    /// </summary>
    /// <param name="policyBuilder">设置策略的实例</param>
    /// <returns></returns>
    PolicyBuilder<HttpResponseMessage> BuildPolicy(PolicyBuilder<HttpResponseMessage> policyBuilder);
}
