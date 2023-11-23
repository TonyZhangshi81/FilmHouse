using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public abstract class EntitiesBase
{
    [Column(Order = 1)]
    [Required]
    public RequestIdVO RequestId { get; set; }

    [Column(Order = 98)]
    [Required]
    public SysDateTimeVO CreatedOn { get; set; }

    [Column(Order = 99)]
    public SysDateTimeVO UpDatedOn { get; set; }

}

