using System.Security.Claims;
using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static FilmHouse.App.Presentation.Web.UI.Models.MineIndexViewModel;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class MineController : Controller
{
    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public MineController(IMediator mediator)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
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
}
