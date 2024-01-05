using System.Text.Json.Serialization;
using FilmHouse.Core.DataAnnotations;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;
using SysDataAnnotations = System.ComponentModel.DataAnnotations;

namespace FilmHouse.Core.Services.HttpClients.Models;

public partial class RequestMetadataModel : IRequestMetadata
{
    /// <summary>
    /// 請求ID
    /// </summary>
    [JsonPropertyName("requestId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [SysDataAnnotations.Display(Name = nameof(Resources.RequestId), ResourceType = typeof(Resources))]
    [Required]
    public RequestIdVO? RequestId { get; set; }

}
