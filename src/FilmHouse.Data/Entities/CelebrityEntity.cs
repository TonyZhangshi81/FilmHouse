using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Data.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class CelebrityEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public Guid CelebrityId { get; set; }

    [Column(Order = 3)]
    [Required]
    public string Name { get; set; }

    [Column(Order = 4)]
    public string Aka { get; set; }

    [Column(Order = 5)]
    public string NameEn { get; set; }

    [Column(Order = 6)]
    public string AkaEn { get; set; }

    [Column(Order = 7)]
    public int Gender { get; set; }

    [Column(Order = 8)]
    public string Occupation { get; set; }

    [Column(Order = 9)]
    public string Birthday { get; set; }

    [Column(Order = 10)]
    public string Deathday { get; set; }

    [Column(Order = 11)]
    public string BornPlace { get; set; }

    [Column(Order = 12)]
    public string Family { get; set; }

    [Column(Order = 13)]
    public string Avatar { get; set; }

    [Column(Order = 14)]
    public string Works { get; set; }

    [Column(Order = 15)]
    public string DoubanID { get; set; }

    [Column(Order = 16)]
    public string IMDb { get; set; }

    [Column(Order = 17)]
    public string Summary { get; set; }

    [Column(Order = 18)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 19)]
    public Int16 ReviewStatus { get; set; }

    [Column(Order = 20)]
    public string ReviewNote { get; set; }

}
