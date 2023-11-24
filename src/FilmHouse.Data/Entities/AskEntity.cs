using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Data.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class AskEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public AskIdVO AskId { get; set; }

    [Column(Order = 3)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 4)]
    [Required]
    public MovieIdVO MovieId { get; set; }

    [Column(Order = 5)]
    public RequestTimeVO RequestTime { get; set; }

    [Column(Order = 6)]
    public RequestWithVO RequestWith { get; set; }

    [Column(Order = 7)]
    public NoteVO Note { get; set; }

    [Column(Order = 8)]
    public bool Status { get; set; }

}
