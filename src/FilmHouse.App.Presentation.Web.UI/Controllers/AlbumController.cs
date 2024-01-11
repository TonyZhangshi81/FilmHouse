using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class AlbumController : Controller
{
    #region Initizalize

    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public AlbumController(IMediator mediator)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
    }

    #endregion Initizalize

    #region 专辑首页

    //
    // GET: /Album/Index/
    public async Task<IActionResult> Index()
    {
        var command = new FilmHouse.Commands.Album.DisplayCommand();
        var display = await this._mediator.Send(command);

        var viewModel = new AlbumIndexViewModel();
        foreach (var item in display.Albums)
        {
            viewModel.Albums.Add(AlbumIndexViewModel.AlbumDiscViewModel.FromEntity(item, display.AlbumFollows.Where(d => d.AlbumId == item.AlbumId).Select(d => d.FollowCount).First()));
        }

        return View(viewModel);
    }

    #endregion
}
