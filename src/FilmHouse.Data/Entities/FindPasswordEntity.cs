using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class FindPasswordEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public FindIdVO FindId { get; set; }

    [Column(Order = 3)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 5)]
    [Required]
    public TokenStringVO Token { get; set; }

    [Column(Order = 6)]
    [Required]
    public EmailAddressVO EmailAddress { get; set; }

    [Column(Order = 7)]
    [Required]
    public ExpiryTimeVO ExpiryTime { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public virtual UserAccountEntity UserAccount { get; set; }
}
