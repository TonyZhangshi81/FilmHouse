using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class MarkEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public MarkIdVO MarkId { get; set; }

    [Column(Order = 3)]
    [Required]
    public MarkTypeVO Type { get; set; }

    [Column(Order = 4)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 5)]
    [Required]
    public MarkTargetIdVO Target { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public virtual UserAccountEntity UserAccount { get; set; }
}
