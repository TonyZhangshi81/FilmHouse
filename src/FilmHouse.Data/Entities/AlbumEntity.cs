using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class AlbumEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public Guid AlbumId { get; set; }

    [Column(Order = 3)]
    [Required]
    public string Title { get; set; }

    [Column(Order = 4)]
    public Guid UserId { get; set; }

    [Column(Order = 5)]
    public string Item { get; set; }

    [Column(Order = 6)]
    public string Summary { get; set; }

    [Column(Order = 7)]
    public string Cover { get; set; }

    [Column(Order = 8)]
    public int Visit { get; set; }

}
