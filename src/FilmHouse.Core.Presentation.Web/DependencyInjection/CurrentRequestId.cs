#nullable enable
using FilmHouse.Core.Constants;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace FilmHouse.Core.Presentation.Web.DependencyInjection;

/// <summary>
/// 是用于获取在当前处理中统一使用的请求ID的接口。
/// </summary>
/// <remarks>
/// 通过使用这个类型，获取当前的处理是哪个请求ID。
/// 获取HttpContext.Items中存储的信息。
/// </remarks>
public class CurrentRequestId : ICurrentRequestId
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// <see cref="CurrentRequestId"/>的新实例。
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public CurrentRequestId(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// <see cref="IHttpContextAccessor"/>从<see cref="HttpContext"/>取得
    /// </summary>
    /// <exception cref="InvalidOperationException"><see cref="HttpContext"/>的实例不能获得的情况下被抛出。</exception>
    private HttpContext CurrentContext
    {
        get
        {
            var httpContext = Guard.GetNotNull(this._httpContextAccessor.HttpContext, nameof(HttpContext));
            return httpContext;
        }
    }

    /// <summary>
    /// 获取当前处理的请求ID。
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"><see cref="HttpContext.Items"/>中设想的键没有被存储的情况下被抛出。</exception>
    public RequestIdVO Get()
    {
        var key = HttpContextItemNames.CurrentRequestId;
        var value = this.CurrentContext.Items[key];
        if (value == null)
        {
            throw new KeyNotFoundException($"The key {key} is not stored in HttpContext.Items.");
        }
        if (value is not RequestIdVO)
        {
            throw new NotSupportedException($"The key {key} stored in HttpContext.Items is not of type {typeof(RequestIdVO).Name}.");
        }

        return (RequestIdVO)value;
    }

    /// <summary>
    /// 获取当前处理的请求ID。
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryGet(out RequestIdVO? value)
    {
        value = default(RequestIdVO);
        var val = this.CurrentContext.Items[HttpContextItemNames.CurrentRequestId];
        if (val == null)
        {
            return false;
        }
        if (val is not RequestIdVO)
        {
            return false;
        }
        value = (RequestIdVO)val;
        return true;
    }

    /// <summary>
    /// 设置当前处理的请求ID。
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="InvalidOperationException"><see cref="HttpContext.Items"/>中已经登录的状态，发生第二次以后的登录的情况会被抛掉。</exception>
    public void Set(RequestIdVO value)
    {
        var key = HttpContextItemNames.CurrentRequestId;
        if (this.CurrentContext.Items[key] != null)
        {
            throw new InvalidOperationException($"The key {key} stored in HttpContext.Items is already registered. You cannot register twice.");
        }
        this.CurrentContext.Items[key] = value;
    }
}
