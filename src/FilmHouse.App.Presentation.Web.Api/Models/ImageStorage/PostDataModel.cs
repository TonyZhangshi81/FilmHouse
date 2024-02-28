using System.Text.Json.Serialization;
using FilmHouse.Core.DataAnnotations;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;
using MongoDB.Bson;
using SysDataAnnotations = System.ComponentModel.DataAnnotations;

namespace FilmHouse.App.Presentation.Web.Api.Models.ImageStorage.Post;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public partial class RequestDataModel
{
    /// <summary>
    /// 文件名（带扩展名）
    /// </summary>
    [JsonPropertyName("FileName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required]
    public virtual string? FileName { get; set; }

    /// <summary>
    /// 文件名（带扩展名）
    /// </summary>
    [JsonPropertyName("FilePath")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required]
    public virtual string? FilePath { get; set; }
}


[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public partial class ResponseDataModel
{
    [JsonPropertyName("InternalId")]
    public virtual ObjectId InternalId { get; set; }

    [JsonPropertyName("IsSuccess")]
    public virtual bool IsSuccess { get; set; }

}