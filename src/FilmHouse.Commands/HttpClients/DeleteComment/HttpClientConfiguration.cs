using System.Text.Json.Serialization;
using FilmHouse.Core.Services.HttpClients;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;
using FilmHouse.Core.DataAnnotations;
using Microsoft.Extensions.Configuration;
using SysDataAnnotations = System.ComponentModel.DataAnnotations;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Entities;

namespace FilmHouse.Commands.HttpClients.DeleteComment;

public partial class HttpClientConfiguration : HttpClientConfigurationBase
{
    public HttpClientConfiguration(IConfiguration configuration)
    : base(configuration)
    {
    }

    protected override string GetBaseAddress() => "/api/comment/Delete";
}


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