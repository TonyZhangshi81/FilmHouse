using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class CelebrityEntity : EntitiesBase
{
    public CelebrityEntity()
    {
        Works = new HashSet<WorkEntity>();
    }

    [Column(Order = 2)]
    [Required]
    public CelebrityIdVO CelebrityId { get; set; }

    [Column(Order = 3)]
    [Required]
    public CelebrityNameVO Name { get; set; }

    [Column(Order = 4)]
    public CelebrityAkaVO Aka { get; set; }

    [Column(Order = 5)]
    public CelebrityNameEnVO NameEn { get; set; }

    [Column(Order = 6)]
    public CelebrityAkaEnVO AkaEn { get; set; }

    [Column(Order = 7)]
    public GenderVO Gender { get; set; }

    [Column(Order = 8)]
    public ProfessionsVO Professions { get; set; }

    [Column(Order = 9)]
    public BirthdayVO Birthday { get; set; }

    [Column(Order = 10)]
    public DeathdayVO Deathday { get; set; }

    [Column(Order = 11)]
    public BornPlaceVO BornPlace { get; set; }

    [Column(Order = 12)]
    public FamiliesVO Family { get; set; }

    [Column(Order = 13)]
    public StarAvatarVO Avatar { get; set; }

    [Column(Order = 14)]
    public WorksVO WorksId { get; set; }

    [Column(Order = 15)]
    public DoubanIDVO DoubanID { get; set; }

    [Column(Order = 16)]
    public IMDbVO IMDb { get; set; }

    [Column(Order = 17)]
    public SummaryVO Summary { get; set; }

    [Column(Order = 18)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 19)]
    public ReviewStatusVO ReviewStatus { get; set; }

    [Column(Order = 20)]
    public NoteVO Note { get; set; }




    public virtual ICollection<WorkEntity> Works { get; set; }

    public virtual UserAccountEntity UserAccount { get; set; }


}
