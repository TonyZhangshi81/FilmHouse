using System.Text.Json.Serialization;
using FilmHouse.Core.DataAnnotations;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;
using SysDataAnnotations = System.ComponentModel.DataAnnotations;

namespace FilmHouse.App.Presentation.Web.Api.Models.Comment.Delete;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public partial class RequestDataModel
{
    /// <summary>
    /// 评论ID
    /// </summary>
    [JsonPropertyName("CommentId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [SysDataAnnotations.Display(Name = nameof(Resources.CommentId), ResourceType = typeof(Resources))]
    [Required]
    public virtual CommentIdVO? CommentId { get; set; }

}


/// <summary>
/// 
/// </summary>
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public partial class ResponseDataModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("IsSuccess")]
    public virtual bool IsSuccess { get; set; }

}