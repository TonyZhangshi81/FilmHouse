using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class WorkEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public Guid WorkId { get; set; }

    [Column(Order = 3)]
    [Required]
    public Guid MovieId { get; set; }

    [Column(Order = 4)]
    [Required]
    public Guid CelebrityId { get; set; }

    [Column(Order = 5)]
    public Int16 Type { get; set; }

}
