using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Data.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

/// <summary>
/// 影集
/// </summary>
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class AlbumEntity : EntitiesBase
{
    /// <summary>
    /// ID KEY
    /// </summary>
    [Column(Order = 2)]
    [Required]
    public AlbumIdVO AlbumId { get; set; }

    /// <summary>
    /// 影集的名字
    /// </summary>
    [Column(Order = 3)]
    [Required]
    public AlbumTitleVO Title { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    [Column(Order = 4)]
    public UserIdVO UserId { get; set; }

    /// <summary>
    /// 影集中收录的电影信息集合（json格式）
    /// </summary>
    [Column(Order = 5)]
    public AlbumJsonItemsVO Items { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    [Column(Order = 6)]
    public SummaryVO Summary { get; set; }

    /// <summary>
    /// 封面
    /// </summary>
    [Column(Order = 7)]
    public CoverVO Cover { get; set; }

    /// <summary>
    /// 关注度
    /// </summary>
    [Column(Order = 8)]
    public AmountAttentionVO AmountAttention { get; set; }

}
