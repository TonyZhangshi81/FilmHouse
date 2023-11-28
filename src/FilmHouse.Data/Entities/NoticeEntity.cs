using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Data.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class NoticeEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public NoticeIdVO NoticeId { get; set; }

    [Column(Order = 3)]
    [Required]
    public ContentVO Content { get; set; }

    [Column(Order = 4)]
    [Required]
    public ResourceIdVO ResourceId { get; set; }

    [Column(Order = 5)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 6)]
    public NoticeFlagVO Flag { get; set; }

}
