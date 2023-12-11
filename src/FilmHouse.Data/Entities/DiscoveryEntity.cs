using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class DiscoveryEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public DiscoveryIdVO DiscoveryId { get; set; }

    [Column(Order = 3)]
    [Required]
    public MovieIdVO MovieId { get; set; }

    [Column(Order = 4)]
    [Required]
    public DiscoveryAvatarVO Avatar { get; set; }

    [Column(Order = 5)]
    [Required]
    public SortOrderVO Order { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public virtual MovieEntity Movie { get; set; }

}
