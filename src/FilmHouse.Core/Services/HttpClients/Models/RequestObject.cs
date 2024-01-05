using System.Text.Json.Serialization;
using FilmHouse.Core.DataAnnotations;

namespace FilmHouse.Core.Services.HttpClients.Models;

/// <summary>
/// 定义传递到WebAPI的请求类型的类。
/// </summary>
/// <typeparam name="TMetadata">元数据的类型</typeparam>
/// <typeparam name="TRequest">要求的类型</typeparam>
public class RequestObject<TMetadata, TRequest> : IRequestObject<TMetadata>
    where TMetadata : IRequestMetadata
    where TRequest : class
{
    /// <summary>
    /// <see cref="RequestObject{TMetadata, TRequest}"/>的新实例。
    /// </summary>
    public RequestObject()
    {
    }

    /// <summary>
    /// 设置或获取元数据。
    /// </summary>
    [Required]
    [JsonPropertyName("metadata")]
    public virtual TMetadata? Metadata
    {
        get; set;
    }

    /// <summary>
    /// 设定或取得请求。
    /// </summary>
    [Required]
    [JsonPropertyName("request")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TRequest? Request
    {
        get; set;
    }
}