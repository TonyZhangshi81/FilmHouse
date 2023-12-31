using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.App.Presentation.Web.UI.Models.Components;

namespace FilmHouse.App.Presentation.Web.UI.Models;

public class CelebViewModel
{
    public CelebDiscViewModel Celebrity { get; set; } = new CelebDiscViewModel();

    public IReadOnlyList<MovieEntity> CelebAboutMovies { get; set; }
}

public class CelebDiscViewModel
{
    /// <summary>
    /// 影人ID
    /// </summary>
    public CelebrityIdVO CelebrityId { get; set; }
    /// <summary>
    /// 中文名字
    /// </summary>
    public CelebrityNameVO Name { get; set; }
    /// <summary>
    /// 英文名
    /// </summary>
    public CelebrityNameEnVO NameEn { get; set; }
    /// <summary>
    /// 更多中文名
    /// </summary>
    public CelebrityAkaVO Aka { get; set; }
    /// <summary>
    /// 更多英文名
    /// </summary>
    public CelebrityAkaEnVO AkaEn { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public GenderVO Gender { get; set; }
    public GenderNameVO GenderName { get; set; }   
    /// <summary>
    /// 出生日期
    /// </summary>
    public BirthdayVO Birthday { get; set; }
    /// <summary>
    /// 生卒年月
    /// </summary>
    public DeathdayVO Deathday { get; set; }
    /// <summary>
    /// 出生地
    /// </summary>
    public BornPlaceVO BornPlace { get; set; }
    /// <summary>
    /// 职业
    /// </summary>
    public ProfessionsVO Professions { get; set; }
    /// <summary>
    /// 家庭成员
    /// </summary>
    public FamiliesVO Family { get; set; }
    /// <summary>
    /// 豆瓣ID
    /// </summary>
    public DoubanIDVO DoubanID { get; set; }
    /// <summary>
    /// IMDbID
    /// </summary>
    public IMDbIDVO IMDbID { get; set; }
    /// <summary>
    /// 简介
    /// </summary>
    public SummaryVO Summary { get; set; }
    /// <summary>
    /// 明星海报
    /// </summary>
    public StarAvatarVO Avatar { get; set; }

    /// <summary>
    /// 被收藏的影人
    /// </summary>
    public bool IsCollect { get; set; }

    /// <summary>
    /// 是不是当前影人信息的创建者或是管理员
    /// </summary>
    public bool IsCreate { get; set; }

    public static CelebDiscViewModel FromEntity(CelebrityEntity celeb)
    {
        var viewModel = new CelebDiscViewModel();
        viewModel.CelebrityId = celeb.CelebrityId;
        viewModel.Name = celeb.Name;
        viewModel.NameEn = celeb.NameEn;
        viewModel.Aka = celeb.Aka;
        viewModel.AkaEn = celeb.AkaEn;
        viewModel.Gender = celeb.Gender;
        viewModel.GenderName = celeb.Gender is not null ? celeb.Gender.ToGenderName() : null;
        viewModel.Birthday = celeb.Birthday;
        viewModel.Deathday = celeb.Deathday;
        viewModel.BornPlace = celeb.BornPlace;
        viewModel.Professions = celeb.Professions;
        viewModel.Family = celeb.Family;
        viewModel.DoubanID = celeb.DoubanID;
        viewModel.IMDbID = celeb.IMDbID;
        viewModel.Summary = celeb.Summary;
        viewModel.Avatar = celeb.Avatar;
        return viewModel;
    }
}


