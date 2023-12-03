using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Data.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class WorkEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public WorkIdVO WorkId { get; set; }

    [Column(Order = 3)]
    [Required]
    public MovieIdVO MovieId { get; set; }

    [Column(Order = 4)]
    [Required]
    public CelebrityIdVO CelebrityId { get; set; }

    [Column(Order = 5)]
    public WorkTypeVO Type { get; set; }

}
