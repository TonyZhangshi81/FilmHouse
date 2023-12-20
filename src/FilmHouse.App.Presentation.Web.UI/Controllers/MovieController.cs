using FilmHouse.App.Presentation.Web.UI.Models.Components;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.Web.Controllers;

public class MovieController : Controller
{
    #region Initizalize

    private readonly IMediator _mediator;
    private readonly ISettingProvider _settingProvider;
    private readonly ICurrentRequestId _currentRequestId;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="settingProvider"></param>
    /// <param name="currentRequestId"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public MovieController(IMediator mediator, ISettingProvider settingProvider, ICurrentRequestId currentRequestId)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
        this._settingProvider = Guard.GetNotNull(settingProvider, nameof(ISettingProvider));
        this._currentRequestId = Guard.GetNotNull(currentRequestId, nameof(ICurrentRequestId));
    }

    #endregion Initizalize

    #region 电影详情页

    ////
    //// GET: /Movie/IndexNew/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="movieId"></param>
    /// <returns></returns>
    public async Task<IActionResult> Index(MovieIdVO movieId)
    {
        var command = new FilmHouse.Commands.Movie.DisplayCommand(movieId, User.Identity);
        var display = await _mediator.Send(command);
        if (display.DiscMovie == null)
        {
            return RedirectToAction("NotFound", "Error");
        }

        var model = new MovieViewModel();
        model.Movie = MovieDiscViewModel.FromEntity(display.DiscMovie);

        // 创建者
        model.Movie.IsCreate = display.IsCreate;
        // 想看
        model.Movie.IsPlan = display.IsPlan;
        model.Movie.PlanCount = display.PlanCount;
        // 看过
        model.Movie.IsFinish = display.IsFinish;
        model.Movie.FinishCount = display.FinishCount;
        // 喜欢
        model.Movie.IsFavor = display.IsFavor;
        model.Movie.FavorCount = display.FavorCount;

        return View(model);
    }

    #endregion
}