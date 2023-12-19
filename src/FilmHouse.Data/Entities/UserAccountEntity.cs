using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class UserAccountEntity : EntitiesBase
{
    public UserAccountEntity()
    {
        Albums = new HashSet<AlbumEntity>();
        Asks = new HashSet<AskEntity>();
        Celebrities = new HashSet<CelebrityEntity>();
        Comments = new HashSet<CommentEntity>();
        Movies = new HashSet<MovieEntity>();
        Notices = new HashSet<NoticeEntity>();
        Resources = new HashSet<ResourceEntity>();
        Marks = new HashSet<MarkEntity>();
    }

    [Column(Order = 2)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 3)]
    [Required]
    public AccountNameVO Account { get; set; }

    [Column(Order = 4)]
    [Required]
    public PasswordHashVO PasswordHash { get; set; }

    [Column(Order = 5)]
    [Required]
    public EmailAddressVO EmailAddress { get; set; }

    [Column(Order = 6)]
    public UserAvatarVO Avatar { get; set; }

    [Column(Order = 7)]
    public CoverVO Cover { get; set; }

    [Column(Order = 8)]
    public IsAdminVO IsAdmin { get; set; }

    [Column(Order = 9)]
    public LastLoginIpVO LastLoginIp { get; set; }

    [Column(Order = 10)]
    public LastLoginTimeVO LastLoginTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<AlbumEntity> Albums { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<AskEntity> Asks { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<CelebrityEntity> Celebrities { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<CommentEntity> Comments { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<MovieEntity> Movies { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<NoticeEntity> Notices { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<ResourceEntity> Resources { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<MarkEntity> Marks { get; set; }

}

