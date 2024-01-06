using FilmHouse.Core.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Core.Services.HttpClients;

/// <summary>
/// 将生成<see cref="IHttpClientFactory"/>的实例改为<see cref="HttpClientWrapper"/>的类。
/// </summary>
/// <remarks>
/// 用DI替换实例
/// </remarks>
public class HttpClientFactoryWrapper : IFilmHouseHttpClientFactory
{
    private readonly IHttpClientFactory _factory;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// <see cref="HttpClientFactoryWrapper"/>
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <exception cref="NotSupportedException">
    /// 在无法获得<see cref="IFilmHouseHttpClientFactory"/>的实例时抛异常。
    /// 主要是在没有设置调用WebAPI的时候发生。
    /// </exception>
    public HttpClientFactoryWrapper(IServiceProvider serviceProvider)
    {
        var factory = serviceProvider.GetService<IHttpClientFactory>();
        if (factory == null)
        {
            throw new NotSupportedException("Cannot get an instance of IHttpClientFactory. Make sure you are set up to call the WebAPI.");
        }
        this._factory = factory;
        this._serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="functionId"></param>
    /// <returns></returns>
    public IFilmHouseHttpClient CreateClient(string functionId)
    {
        var httpClient = Guard.GetNotNull(this._factory.CreateClient(functionId), $"{nameof(IHttpClientFactory)}:{functionId}");
        var wrapper = new HttpClientWrapper(httpClient);
        return wrapper;
    }
}
