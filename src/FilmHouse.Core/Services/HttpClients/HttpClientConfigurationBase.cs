using FilmHouse.Core.Constants;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using Microsoft.Extensions.Configuration;
using Polly;

namespace FilmHouse.Core.Services.HttpClients;

/// <summary>
/// 在生成<see cref="IFilmHouseHttpClientFactory"/>到<see cref="HttpClient"/>的实例时进行设置的基类。
/// </summary>
public abstract class HttpClientConfigurationBase : IFilmHouseHttpClientConfiguration
{
    private readonly IConfiguration _configurtion;

    /// <summary>
    /// 获取设置信息的实例
    /// </summary>
    protected IConfiguration Configurtion { get => this._configurtion; }

    /// <summary>
    /// <see cref="HttpClientConfigurationBase"/>的新实例。
    /// </summary>
    /// <param name="configurtion"></param>
    protected HttpClientConfigurationBase(IConfiguration configurtion)
    {
        this._configurtion = configurtion;
    }

    /// <summary>
    /// 作为httpClient的名字，当依赖注入时使用此名作为各个configureClient的匹配名，以此将httpClient与其对应的configure进行关联
    /// </summary>
    /// <remarks>
    /// 使用规则是取用各个configureClient所在的namespace词尾作为functionId的有效设定(使用谓语加宾语的方式命名namespace词尾，避免重复)
    /// </remarks>
    public virtual string FunctionId
    {
        get
        {
            var functionId = this.GetType().Namespace!.Split(".", StringSplitOptions.RemoveEmptyEntries).Reverse().FirstOrDefault();
            Guard.Requires(functionId != null, () => new NotSupportedException($"The function ID could not be obtained from the namespace [{this.GetType().Namespace}]."));
            return functionId!;
        }
    }

    /// <summary>
    /// 获取调用目标WebAPI时指定的API密钥。
    /// </summary>
    public virtual WebApiKeyVO ApiKey
    {
        get
        {
            var key = new WebApiKeyVO(this._configurtion.GetValue<string>($"WebApi:Apikey:Outbound"));
            return key;
        }
    }

    /// <summary>
    /// 设置调用WebAPI时的验证信息。
    /// </summary>
    /// <remarks>
    /// 作为标准动作，在HTTP头部设定了x-filmhouse-api-key。
    /// </remarks>
    public virtual void SetApiKey(HttpClient httpClient)
    {
        if (this.ApiKey != null)
        {
            httpClient.DefaultRequestHeaders.Add(HttpContextItemNames.ApiKeyRequestHeaderItem, this.ApiKey.AsPrimitive());
        }
    }

    /// <summary>
    /// 获取目标WebAPI调用超时的时间。
    /// </summary>
    public virtual TimeSpan? Timeout
    {
        get
        {
            var timeout = this._configurtion.GetValue<TimeSpan?>($"WebApi:RetryPolicy:Timeout");
            return timeout ?? this._configurtion.GetValue<TimeSpan?>("WebApi:DefaultTimeout");
        }
    }

    /// <summary>
    /// 处理失败时，取得进行重试时的重试执行次数。
    /// </summary>
    /// <remarks>
    /// 不存在定义值的情况下为0次。
    /// </remarks>
    public virtual int RetryCount => this._configurtion.GetValue<int?>($"WebApi:RetryPolicy:RetryCount") ?? 0;

    /// <summary>
    /// 获取判定处理失败的状态码。
    /// </summary>
    public virtual ICollection<int> RetryTargetStatusCodes
    {
        get
        {
            var defaultCodes = this._configurtion.GetSection($"WebApi:DefaultStatusCodes").GetChildren().Select(_ => int.Parse(_.Value));
            var codes = new HashSet<int>(defaultCodes);
            var definedCodes = this._configurtion.GetSection($"WebApi:RetryPolicy:StatusCodes").GetChildren().Select(_ => int.Parse(_.Value));
            if (definedCodes == null)
            {
                return codes;
            }
            codes.UnionWith(definedCodes);
            return codes;
        }
    }

    /// <summary>
    /// 获取被定义的代理使用与否。
    /// </summary>
    /// <remarks>
    /// 在没有设定的情况下，不使用代理服务器。
    /// </remarks>
    public virtual bool UseProxy => this._configurtion.GetValue<bool?>($"WebApi:Proxy:Use") ?? false;

    /// <summary>
    /// 获取定义的代理地址。
    /// </summary>
    /// <remarks>
    /// <see cref="UseProxy"/>为true时才获取。
    /// </remarks>
    public virtual string ProxyAddress => this.UseProxy ? this._configurtion.GetValue<string>($"WebApi:Proxy:Address") : null;

    /// <summary>
    /// <see cref="HttpClient"/>的策略设定。
    /// </summary>
    /// <remarks>
    /// 404:确定是否在NotFund的情况下重试。
    /// </remarks>
    /// <param name="policyBuilder">设置策略的实例</param>
    /// <returns></returns>
    public virtual PolicyBuilder<HttpResponseMessage> BuildPolicy(PolicyBuilder<HttpResponseMessage> policyBuilder)
    {
        return policyBuilder.OrResult(_ => this.RetryTargetStatusCodes.Contains((int)_.StatusCode));
    }

    /// <summary>
    /// 获取URI。
    /// </summary>
    /// <returns></returns>
    public virtual Uri GetApiUri()
    {
        var rootAddress = this._configurtion.GetValue<Uri>($"WebApi:BaseAddress");
        var baseAddress = this.GetBaseAddress(); //this._settingProvider.GetValue($"WebApi:{this.FunctionId}:BaseAddress");
        return new Uri(rootAddress!, baseAddress);
    }

    protected abstract string GetBaseAddress();
}
