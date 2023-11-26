using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class CodeMastEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public CodeGroupVO Group { get; set; }

    [Column(Order = 3)]
    [Required]
    public CodeKeyVO Code { get; set; }

    [Column(Order = 4)]
    [Required]
    public CodeValueVO Name { get; set; }

    [Column(Order = 5)]
    [Required]
    public SortOrderVO Order { get; set; }

}
