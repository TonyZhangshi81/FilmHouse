using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using k8s;

namespace FilmHouse.App.Presentation.Web.UI.Models;

/// <summary>
/// 用于管理的专辑
/// </summary>
public class ManageAlbumViewModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "请输入 专辑名称。")]
    [Display(Name = "专辑名称")]
    public string Title { get; set; }

    [Display(Name = "专辑简介")]
    [DataType(DataType.MultilineText)]
    public string Summary { get; set; }

    [Display(Name = "专辑封面")]
    public string Cover { get; set; }

    [Display(Name = "创建人")]
    public string UserId { get; set; }
    public string UserAccount { get; set; }

    public string Item { get; set; }

    [Display(Name = "创建时间")]
    public string CreateTime { get; set; }

    [Display(Name = "修改时间")]
    public string AlterTime { get; set; }

    /*
    public ManageAlbumViewModel(tbl_Album album)
    {
        Id = album.album_Id;
        Title = album.album_Title;
        Summary = album.album_Summary;
        Cover = album.album_Cover;
        UserId = album.album_User;
        UserAccount = AccountManager.GetAccount(album.album_User);
        CreateTime = ((DateTime)album.album_Time).Date.ToShortDateString();
        AlterTime = ((DateTime)album.album_AlterTime).Date.ToShortDateString();
        Item = album.album_Item;
    }
    */

    public ManageAlbumViewModel()
    {
    }
}

/// <summary>
/// 用户展示信息的专辑
/// </summary>
public class AlbumViewModel
{
    public AlbumDiscViewModel Album { get; set; } = new AlbumDiscViewModel();
}

/// <summary>
/// 用户展示信息的专辑
/// </summary>
public class AlbumDiscViewModel
{
    public AlbumIdVO AlbumId { get; set; }

    public AlbumTitleVO Title { get; set; }

    public SummaryVO Summary { get; set; }

    public CoverVO Cover { get; set; }

    public UserIdVO UserId { get; set; }

    public AlbumJsonItemsVO ItemJson { get; set; }

    public CreatedOnVO CreatedOn { get; set; }

    public List<AlbumItemViewModel> Items { get; set; }

    public List<AlbumListItem> Albums { get; set; }

    public static AlbumDiscViewModel FromEntity(AlbumEntity album)
    {
        var viewModel = new AlbumDiscViewModel();
        viewModel.AlbumId = album.AlbumId;
        viewModel.Title = album.Title;
        viewModel.Summary = album.Summary;
        viewModel.Cover = album.Cover;
        viewModel.UserId = album.UserId;
        viewModel.ItemJson = album.Items;
        viewModel.CreatedOn = album.CreatedOn;
        return viewModel;
    }

}

/// <summary>
/// 专辑内项目
/// </summary>
public class AlbumItemViewModel
{
    public string Movie { get; set; }
    public string Note { get; set; }
    public string Time { get; set; }

    public MovieIndexViewModel MovieInfo { get; set; }
}

/// <summary>
/// 所有专辑列表
/// </summary>
public class AlbumListItem
{
    public string Cover { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Id { get; set; }
    public string UserId { get; set; }
    public string UserAccount { get; set; }
    public int VisitCount { get; set; }
    public int FollowCount { get; set; }

    /*
    public AlbumListItem(tbl_Album album)
    {
        Cover = album.album_Cover;
        Id = album.album_Id;
        Title = album.album_Title;
        Summary = album.album_Summary;
        if (!AccountManager.IsAdmin(album.album_User))
        {
            UserId = album.album_User;
            UserAccount = AccountManager.GetAccount(album.album_User);
        }
        VisitCount = (int)album.album_Visit;
        MR_DataClassesDataContext _db = new MR_DataClassesDataContext();
        FollowCount = _db.tbl_Mark.Where(m => m.mark_Target == album.album_Id && m.mark_Type == 7).Count();
    }
    */

    public AlbumListItem()
    {
    }
}