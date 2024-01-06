#nullable enable
using System.Text;
using FilmHouse.Core.Utils;

namespace FilmHouse.Core.Services.HttpClients;

/// <summary>
/// 
/// </summary>
public class JsonStringContent : StringContent
{
    /// <summary>
    /// <see cref="JsonStringContent"/>
    /// </summary>
    /// <param name="requestData">传递到WebAPI的JSON字符串的原始实例</param>
    public JsonStringContent(object? requestData)
        : base(requestData?.ToJsonString() ?? "")
    {
    }

    /// <summary>
    /// <see cref="JsonStringContent"/>
    /// </summary>
    /// <param name="requestData">传递到WebAPI的JSON字符串的原始实例</param>
    /// <param name="encoding">请求的编码</param>
    public JsonStringContent(object? requestData, Encoding? encoding)
        : base(requestData?.ToJsonString() ?? "", encoding)
    {
    }

    /// <summary>
    /// <see cref="JsonStringContent"/>
    /// </summary>
    /// <param name="requestData">传递到WebAPI的JSON字符串的原始实例</param>
    /// <param name="encoding">请求的编码</param>
    /// <param name="mediaType">请求的媒体类型</param>
    public JsonStringContent(object? requestData, Encoding? encoding, string? mediaType)
        : base(requestData?.ToJsonString() ?? "", encoding, mediaType ?? "")
    {
        Guard.Requires<NotSupportedException>(requestData == null || (requestData != null && requestData.GetType().IsClass && requestData.GetType() != typeof(string)));
        this.Content = requestData;
    }

    /// <summary>
    /// 递送到WebAPI的JSON字码串的原始实例取得。
    /// </summary>
    public object? Content { get; }
}
