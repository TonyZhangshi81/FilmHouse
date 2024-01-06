#nullable enable
using System.Text.Json.Serialization;
using FilmHouse.Core.DataAnnotations;

namespace FilmHouse.Core.Services.HttpClients.Models;

/// <summary>
/// 定义从WebAPI返回的响应类型的类。
/// </summary>
/// <typeparam name="TMetadata">元数据的类型</typeparam>
/// <typeparam name="TResponse">回应的类型</typeparam>
public class ResponseObject<TMetadata, TResponse>
    where TMetadata : IResponseMetadata
    where TResponse : class
{
    /// <summary>
    /// 设置或获取元数据。
    /// </summary>
    [Required]
    [JsonPropertyName("metadata")]
    public TMetadata? Metadata { get; set; }

    /// <summary>
    /// 设置或获取响应。
    /// </summary>
    [JsonPropertyName("response")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TResponse? Response { get; set; }
}