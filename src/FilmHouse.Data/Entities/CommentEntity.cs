using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class CommentEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public Guid CommentId { get; set; }

    [Column(Order = 3)]
    [Required]
    public Guid UserId { get; set; }

    [Column(Order = 4)]
    [Required]
    public Guid MovieId { get; set; }

    [Column(Order = 5)]
    public string Content { get; set; }

    [Column(Order = 6)]
    public DateTime CommentTime { get; set; }

}
