using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public abstract class EntitiesBase
{
    [Column(Order = 1)]
    [Required]
    public Guid RequestId { get; set; }

    [Column(Order = 98)]
    [Required]
    public DateTime CreatedOn { get; set; }

    [Column(Order = 99)]
    public DateTime? UpDatedOn { get; set; }

}

