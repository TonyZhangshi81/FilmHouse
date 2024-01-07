using System.Text.Json.Serialization;
using FilmHouse.Core.DataAnnotations;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;
using SysDataAnnotations = System.ComponentModel.DataAnnotations;

namespace FilmHouse.Commands.HttpClients.Comment;

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
    public virtual CommentIdVO CommentId { get; set; }

}