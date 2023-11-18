using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class MarkEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public Guid MarkId { get; set; }

    [Column(Order = 3)]
    [Required]
    public Int16 Type { get; set; }

    [Column(Order = 4)]
    [Required]
    public Guid UserId { get; set; }

    [Column(Order = 5)]
    [Required]
    public Guid Target { get; set; }

    [Column(Order = 6)]
    public DateTime Time { get; set; }
}
