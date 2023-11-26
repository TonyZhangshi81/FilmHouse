using FilmHouse.Data.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class UserAccountEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 3)]
    [Required]
    public string Account { get; set; }

    [Column(Order = 4)]
    [Required]
    public string Password { get; set; }

    [Column(Order = 5)]
    [Required]
    public string EmailAddress { get; set; }

    [Column(Order = 6)]
    public string Avatar { get; set; }

    [Column(Order = 7)]
    public CoverVO Cover { get; set; }

    [Column(Order = 8)]
    public bool IsAdmin { get; set; }
}

