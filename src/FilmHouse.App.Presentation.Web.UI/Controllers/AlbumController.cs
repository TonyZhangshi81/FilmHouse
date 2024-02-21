using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static FilmHouse.App.Presentation.Web.UI.Models.AlbumDetailViewModel;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class AlbumController : Controller
{
    #region Initizalize

    private readonly IMediator _mediator;
    private readonly ISettingProvider _settingProvider;
    private readonly ICodeProvider _codeProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="settingProvider"></param>
    /// <param name="codeProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public AlbumController(IMediator mediator, ISettingProvider settingProvider, ICodeProvider codeProvider)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
        this._settingProvider = Guard.GetNotNull(settingProvider, nameof(ISettingProvider));
        this._codeProvider = Guard.GetNotNull(codeProvider, nameof(ICodeProvider));
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
    public async Task<IActionResult> Detail(AlbumIdVO albumId, int page = 1)
    {
        var pagingSize = this._settingProvider.GetValue(ConfigKeyVO.Keys.AlbumPageMovieMax).CastTo<int>();

        var searchCommand = new FilmHouse.Commands.Album.DetailCommand(albumId, pagingSize, page);
        var display = await this._mediator.Send(searchCommand);

        if (display.Album == null)
        {
            return RedirectToAction("NotFound", "Error");
        }

        var viewModel = new AlbumDetailViewModel();
        // 单页显示件数
        viewModel.PagingSize = pagingSize;
        // 当前页码
        viewModel.Page = page;
        // 查询总页数
        viewModel.PagingCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(display.SearchCount) / pagingSize));
        // 查询总件数
        viewModel.Count = display.SearchCount;
        // 影集信息
        viewModel.Album = AlbumDetailViewModel.AlbumDiscViewModel.FromEntity(display.Album);
        // 是否为创建者
        viewModel.Album.IsCreate = display.IsCreate;
        // 是否取消关注
        viewModel.Album.HasFollow = display.HasFollow;

        // 编辑用
        viewModel.AddAlbum.AlbumId = display.Album.AlbumId;

        // 影集明细记录存在
        if (display.Movies != null)
        {
            foreach (var movie in display.Movies)
            {
                var item = AlbumDetailViewModel.MovieListItem.FromEntity(movie);
                item.GenresValue = movie.Genres.AsCodeElement(this._codeProvider, GenresVO.Group).Select(_ => new SelectListItem() { Text = _.Name.AsPrimitive(), Value = _.Code.AsPrimitive() }).ToList();
                viewModel.Movies.Add(item);
            }
        }

        // 关注度加"1"
        var visitCommand = new FilmHouse.Commands.Album.AlbumVisitCommand(albumId);
        await this._mediator.Send(visitCommand);

        return View(viewModel);
    }
    #endregion

    #region 专辑项目增删

    //
    // POST: /Album/AddItem/
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddItem(AlbumAddItemViewModel model, string returnurl)
    {
        var command = new FilmHouse.Commands.Album.AddCommand(model.AlbumId, model.MovieId);
        await this._mediator.Send(command);
        return RedirectToLocal(returnurl);
    }

    #endregion

    private ActionResult RedirectToLocal(string returnUrl)
    {
        if (!Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl) && !string.IsNullOrWhiteSpace(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction("Index", "Home");
    }
}
