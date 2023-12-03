﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class UserAccountEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 3)]
    [Required]
    public AccountNameVO Account { get; set; }

    [Column(Order = 4)]
    [Required]
    public PasswordVO Password { get; set; }

    [Column(Order = 5)]
    [Required]
    public EmailAddressVO EmailAddress { get; set; }

    [Column(Order = 6)]
    public UserAvatarVO Avatar { get; set; }

    [Column(Order = 7)]
    public CoverVO Cover { get; set; }

    [Column(Order = 8)]
    public IsAdminVO IsAdmin { get; set; }
}

