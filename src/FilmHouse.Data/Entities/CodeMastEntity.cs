using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class CodeMastEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public string Type { get; set; }

    [Column(Order = 3)]
    [Required]
    public string CodeId { get; set; }

    [Column(Order = 4)]
    [Required]
    public string CodeValue { get; set; }

}
