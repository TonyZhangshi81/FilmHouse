using FilmHouse.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class ConfigurationEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public ConfigKeyVO Key { get; set; }

    [Column(Order = 3)]
    public ConfigValueVO Value { get; set; }
}
