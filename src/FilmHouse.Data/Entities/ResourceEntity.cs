using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using Castle.Core.Resource;
using FilmHouse.Data.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class ResourceEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public ResourceIdVO ResourceId { get; set; }

    [Column(Order = 3)]
    [Required]
    public ResourceNameVO Name { get; set; }

    [Column(Order = 4)]
    [Required]
    public ResourceContentVO Content { get; set; }

    [Column(Order = 5)]
    public ResourceSizeVO Size { get; set; }

    [Column(Order = 6)]
    public UserIdVO UserId { get; set; }

    [Column(Order = 7)]
    public MovieIdVO MovieId { get; set; }

    [Column(Order = 8)]
    public FavorCountVO FavorCount { get; set; }

    [Column(Order = 9)]
    public ResourceTypeVO Type { get; set; }

    [Column(Order = 10)]
    public ReviewStatusVO ReviewStatus { get; set; }

    [Column(Order = 11)]
    public NoteVO Note { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<NoticeEntity> Notices { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual MovieEntity Movie { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual UserAccountEntity UserAccount { get; set; }


}
