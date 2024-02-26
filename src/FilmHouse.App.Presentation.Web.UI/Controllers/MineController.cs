using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Commands.Mine;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Abstractions;
using static FilmHouse.App.Presentation.Web.UI.Models.MineIndexViewModel;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class MineController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICodeProvider _codeProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="codeProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public MineController(IMediator mediator, ICodeProvider codeProvider)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
        this._codeProvider = Guard.GetNotNull(codeProvider, nameof(ICodeProvider));
    }

    #region 用户主页

    //
    // GET: /Mine/Index/
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userIdentity = User.Identity;
        // 取得登录用户的ID
        var claimsIdentity = userIdentity as ClaimsIdentity;
        var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

        var command = new FilmHouse.Commands.Mine.DisplayCommand(userId);
        var display = await this._mediator.Send(command);
        if (display.DiscUserAccount == null)
        {
            return base.RedirectToAction("NotFound", "Error");
        }

        var model = new MineIndexViewModel();
        // 用戶信息
        model.DiscViewModel = MineDiscViewModel.FromEntity(display.DiscUserAccount);

        // 想看的电影
        model.DiscViewModel.PlanCount = display.MoviePlans.Count();
        model.DiscViewModel.MoviePlans = display.MoviePlans.Select(d => new SelectListItem { Value = d.MovieId.AsPrimitive().ToString(), Text = d.Title.AsPrimitive() }).ToList();

        // 看过的电影
        model.DiscViewModel.FinishCount = display.MovieFinishs.Count();
        model.DiscViewModel.MovieFinishs = display.MovieFinishs.Select(d => new SelectListItem { Value = d.MovieId.AsPrimitive().ToString(), Text = d.Title.AsPrimitive() }).ToList();

        // 喜欢的电影
        model.DiscViewModel.FavorCount = display.MovieFavors.Count();
        model.DiscViewModel.MovieFavors = display.MovieFavors.Select(d => new SelectListItem { Value = d.MovieId.AsPrimitive().ToString(), Text = d.Title.AsPrimitive() }).ToList();

        // 收藏的影人
        model.DiscViewModel.CollectCount = display.CelebCollects.Count();
        model.DiscViewModel.CelebCollects = display.CelebCollects.Select(d => new SelectListItem { Value = d.CelebrityId.AsPrimitive().ToString(), Text = d.Name.AsPrimitive() }).ToList();

        // 专辑
        model.DiscViewModel.AlbumCount = display.Albums.Count();
        model.DiscViewModel.Albums = display.Albums.Select(d => new AlbumListItem
        {
            AlbumId = d.AlbumId,
            Title = d.Title,
            Summary = d.Summary,
            Cover = d.Cover,
            UserId = d.UserId,
            Account = d.UserAccount.Account,
            AmountAttention = d.AmountAttention,
            FollowCount = display.AlbumFollows.Where(_ => _.AlbumId == d.AlbumId).Select(_ => _.FollowCount).FirstOrDefault()
        }).ToList();

        // 评论
        model.DiscViewModel.CommentCount = display.Comments.Count();
        model.DiscViewModel.Comments = display.Comments.Select(d => new CommentListItem { MovieId = d.MovieId, Content = d.Content, CommentTime = d.CommentTime, Title = d.Movie.Title }).ToList();

        return View(model);
    }

    #endregion


    #region 用户电影、影人、资源、求资源、专辑主页

    //
    // GET: /Mine/MineMovie/
    [Authorize]
    public async Task<IActionResult> MineMovie(int type)
    {
        if (type > 4 || type < 1)
        {
            return RedirectToAction("NotFound", "Error");
        }
        ViewBag.MovieType = type;


        var command = new FilmHouse.Commands.Mine.MovieDisplayCommand(type);
        var display = await this._mediator.Send(command);

        // 模板页用户信息（头像、用户名）
        ViewBag.Avatar = display.DiscUserAccount.Avatar;
        ViewBag.Account = display.DiscUserAccount.Account;

        var model = new MineMovieViewModel();
        // 想看的影片数量
        model.DiscViewModel.PlanCount = display.PlanCount;
        // 看过的影片数量
        model.DiscViewModel.FinishCount = display.FinishCount;
        // 喜欢的影片数量
        model.DiscViewModel.FavorCount = display.FavorCount;
        // 自己创建的影片数量
        model.DiscViewModel.CreateCount = display.CreateCount;

        // 影片编辑列表
        model.DiscViewModel.MovieMarks = display.MovieMarks.Select(_ => new MineMovieViewModel.MineMovieDiscViewModel.MovieMark()
        {
            MovieId = _.MovieId,
            Note = _.Note,
            Rating = _.Rating,
            ReviewStatus = _.ReviewStatus,
            Title = _.Title,
            Year = _.Year,
            Directors = _.Directors,
            DoubanID = _.DoubanID,
            // 電影類型（无导航功能）
            GenresValue = GetGenresValue(_.Genres),
            // 喜欢
            IsFavor = _.IsFavor,
            // 看过
            IsFinish = _.IsFinish,
            // 计划
            IsPlan = _.IsPlan,
        }).ToList();

        // 返回影片类型名
        string GetGenresValue(GenresVO genres)
        {
            var genreValues = new StringBuilder();
            genres.AsCodeElement(this._codeProvider, GenresVO.Group).ToList().ForEach(d => genreValues.AppendFormat("{0} ", d.Name.AsPrimitive()));

            return genreValues.ToString();
        }

        return View(model);
    }

    #endregion





}
