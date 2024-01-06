#nullable enable
using System.Text.Json.Serialization;
using FilmHouse.Core.DataAnnotations;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;
using SysDataAnnotations = System.ComponentModel.DataAnnotations;

namespace FilmHouse.Core.Services.HttpClients.Models;

/// <summary>
/// 响应的元数据
/// </summary>
public partial class ResponseMetadataModel : IResponseMetadata
{
    /// <summary>
    /// 获取请求ID。
    /// </summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [SysDataAnnotations.Display(Name = nameof(Resources.Status), ResourceType = typeof(Resources))]
    [Number]
    [NumberDigits(HttpStatusCodeVO.Precision)]
    [Required]
    public virtual HttpStatusCodeVO? Status { get; set; }

    /// <summary>
    /// 获取返还回复的时间。
    /// </summary>
    [JsonPropertyName("timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [SysDataAnnotations.Display(Name = nameof(Resources.ApiResponseTime), ResourceType = typeof(Resources))]
    [Required]
    public virtual ApiResponseTimeVO? Timestamp { get; set; }

    /// <summary>
    /// 设置或获取请求ID。
    /// </summary>
    [JsonPropertyName("requestId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [SysDataAnnotations.Display(Name = nameof(Resources.RequestId), ResourceType = typeof(Resources))]
    [Required]
    public virtual RequestIdVO? RequestId { get; set; }

    /// <summary>
    /// 设置或获取验证中发生的错误内容。
    /// </summary>
    [JsonPropertyName("errors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [SysDataAnnotations.Display(Name = nameof(Resources.ApiResponseErrors), ResourceType = typeof(Resources))]
    public virtual ICollection<MessageTextVO>? Errors { get; set; }
}
