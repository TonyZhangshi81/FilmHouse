using System.Security.Claims;
using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static FilmHouse.App.Presentation.Web.UI.Models.PeopleIndexViewModel;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class PeopleController : Controller
{
    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PeopleController(IMediator mediator)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
    }


    //
    // GET: /People/Index/
    public async Task<IActionResult> Index(UserIdVO userId, string transfer)
    {
        var command = new FilmHouse.Commands.People.DisplayCommand(userId);
        var display = await this._mediator.Send(command);
        if (display.DiscUserAccount == null)
        {
            return base.RedirectToAction("NotFound", "Error");
        }

        // 如果是管理員的情況
        //if (display.DiscUserAccount.IsAdmin.AsPrimitive())
        //{
        //    return Redirect(transfer);
        //}

        var model = new PeopleIndexViewModel();

        // 用戶信息
        model.MineDiscViewModel = UserAccountDiscViewModel.FromEntity(display.DiscUserAccount);
        // 用戶登錄的情況下，顯示以下信息（共同喜好的电影）
        if (User.Identity.IsAuthenticated)
        {
            var userIdentity = User.Identity;
            // 取得登录用户的ID
            var claimsIdentity = userIdentity as ClaimsIdentity;
            var id = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));
            // 如果是登錄用戶
            if (userId == id)
            {
                return RedirectToAction("Index", "Mine");
            }

            // 共同喜好的电影
            model.MineDiscViewModel.CommonCount = display.MovieCommons.Count();
            model.MineDiscViewModel.MovieCommons = display.MovieCommons.Select(d => new MovieListItem() { Avatar = d.Avatar, MovieId = d.MovieId, Title = d.Title }).ToList();
        }

        // 想看的电影
        model.MineDiscViewModel.PlanCount = display.MoviePlans.Count();
        model.MineDiscViewModel.MoviePlans = display.MoviePlans.Select(d => new SelectListItem { Value = d.MovieId.AsPrimitive().ToString(), Text = d.Title.AsPrimitive() }).ToList();

        // 看过的电影
        model.MineDiscViewModel.FinishCount = display.MovieFinishs.Count();
        model.MineDiscViewModel.MovieFinishs = display.MovieFinishs.Select(d => new SelectListItem { Value = d.MovieId.AsPrimitive().ToString(), Text = d.Title.AsPrimitive() }).ToList();

        // 喜欢的电影
        model.MineDiscViewModel.FavorCount = display.MovieFavors.Count();
        model.MineDiscViewModel.MovieFavors = display.MovieFavors.Select(d => new SelectListItem { Value = d.MovieId.AsPrimitive().ToString(), Text = d.Title.AsPrimitive() }).ToList();

        // 收藏的影人
        model.MineDiscViewModel.CollectCount = display.CelebCollects.Count();
        model.MineDiscViewModel.CelebCollects = display.CelebCollects.Select(d => new SelectListItem { Value = d.CelebrityId.AsPrimitive().ToString(), Text = d.Name.AsPrimitive() }).ToList();

        // 专辑
        model.MineDiscViewModel.AlbumCount = display.Albums.Count();
        model.MineDiscViewModel.Albums = display.Albums.Select(d => new AlbumListItem
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
        model.MineDiscViewModel.CommentCount = display.Comments.Count();
        model.MineDiscViewModel.Comments = display.Comments.Select(d => new CommentListItem { MovieId = d.MovieId, Content = d.Content, CommentTime = d.CommentTime, Title = d.Movie.Title }).ToList();

        return View(model);
    }

}
