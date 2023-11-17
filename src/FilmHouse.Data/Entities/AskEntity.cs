using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class AskEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public Guid AskId { get; set; }

    [Column(Order = 3)]
    [Required]
    public Guid UserId { get; set; }

    [Column(Order = 4)]
    [Required]
    public Guid MovieId { get; set; }

    [Column(Order = 5)]
    public DateTime RequestTime { get; set; }

    [Column(Order = 6)]
    public int RequestWith { get; set; }

    [Column(Order = 7)]
    public string Note { get; set; }

    [Column(Order = 8)]
    public bool Status { get; set; }

}
