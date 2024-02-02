using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
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

    #region 专辑详情页
    //
    // GET: /Album/Detail/
    public ActionResult Detail(AlbumIdVO albumId, int page = 1)
    {
        if (!AlbumManager.Exist(id))
        {
            return RedirectToAction("NotFound", "Error");
        }

        tbl_Album tblalbum = _db.tbl_Album.SingleOrDefault(s => s.album_Id == id);
        AlbumViewModel album = new AlbumViewModel(tblalbum);
        if (tblalbum.album_User == AccountManager.GetId(User.Identity.Name))
        {
            album.IsCreator = true;
        }
        if (User.Identity.IsAuthenticated)
        {
            if (_db.tbl_Mark.SingleOrDefault(f => f.mark_Target == id && f.mark_User == AccountManager.GetId(User.Identity.Name) && f.mark_Type == 7) != null)
            {
                album.HasFollow = true;
            }
        }

        List<AlbumItemViewModel> allItem = new List<AlbumItemViewModel>();
        //album.Items = new List<AlbumItemViewModel>();
        allItem = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AlbumItemViewModel>>(album.ItemJson);
        album.Count = allItem.Count;
        album.Items = allItem.Skip((page - 1) * album.ItemSize).Take(album.ItemSize).ToList();
        foreach (var item in album.Items)
        {
            item.MovieInfo = new MovieViewModel(_db.tbl_Movie.SingleOrDefault(m => m.movie_Id == item.Movie));
        }

        album.Page = page;
        album.PagingCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(album.Count) / album.ItemSize));

        if (page > album.PagingCount && album.Items.Count > 0)
        {
            return RedirectToAction("NotFound", "Error");
        }

        AlbumManager.Visit(id);
        return View(album);
    }
    #endregion
}
