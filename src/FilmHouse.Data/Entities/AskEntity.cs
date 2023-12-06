using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Data.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

/// <summary>
/// 资源请求
/// </summary>
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class AskEntity : EntitiesBase
{
    /// <summary>
    /// ID KEY
    /// </summary>
    [Column(Order = 2)]
    [Required]
    public AskIdVO AskId { get; set; }

    /// <summary>
    /// 请求者
    /// </summary>
    [Column(Order = 3)]
    [Required]
    public UserIdVO UserId { get; set; }

    /// <summary>
    /// 影片ID
    /// </summary>
    [Column(Order = 4)]
    [Required]
    public MovieIdVO MovieId { get; set; }

    /// <summary>
    /// 请求时间
    /// </summary>
    [Column(Order = 5)]
    public RequestTimeVO RequestTime { get; set; }

    /// <summary>
    /// 请求数量
    /// </summary>
    [Column(Order = 6)]
    public RequestWithVO RequestWith { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Column(Order = 7)]
    public NoteVO Note { get; set; }

    /// <summary>
    /// 状态（0:未求到; 1:已求到）
    /// </summary>
    [Column(Order = 8)]
    public AskStatusVO Status { get; set; }





    /// <summary>
    /// 
    /// </summary>
    public virtual MovieEntity Movie { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual UserAccountEntity UserAccount { get; set; }

}
