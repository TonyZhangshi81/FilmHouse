using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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
    [Number(HttpStatusCodeVO.TypeName)]
    [NumberDigits(HttpStatusCodeVO.Precision)]
    [Required]
    public virtual HttpStatusCodeVO? Status { get; set; }

    /// <summary>
    /// 获取返还回复的时间。
    /// </summary>
    [JsonPropertyName("timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [SysDataAnnotations.Display(Name = nameof(Resources.Status), ResourceType = typeof(Resources))]

    [System.ComponentModel.DataAnnotations.Display(Name = "タイムスタンプ")]
    [Required(NeedsFullMessage = true)]
    public virtual OffsetDateTime? Timestamp { get; set; }

    /// <summary>
    /// リクエストIDを設定または取得します。
    /// </summary>
    [JsonPropertyName("requestId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [System.ComponentModel.DataAnnotations.Display(Name = "リクエストID")]
    [Required(NeedsFullMessage = true)]
    public virtual RequestId? RequestId { get; set; }

    /// <summary>
    /// 検証で発生したエラー内容を設定または取得します。
    /// </summary>
    [JsonPropertyName("error")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [System.ComponentModel.DataAnnotations.Display(Name = "検証で発生したエラー内容")]
    public virtual ResponseErrorModel? Error { get; set; }
}
