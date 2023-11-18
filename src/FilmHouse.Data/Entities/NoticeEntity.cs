using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class NoticeEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public Guid NoticeId { get; set; }

    [Column(Order = 3)]
    [Required]
    public string Content { get; set; }

    [Column(Order = 4)]
    [Required]
    public Guid ResourceId { get; set; }

    [Column(Order = 5)]
    [Required]
    public Guid UserId { get; set; }

    [Column(Order = 7)]
    public DateTime Time { get; set; }

    [Column(Order = 8)]
    public Int16 Flag { get; set; }

}
