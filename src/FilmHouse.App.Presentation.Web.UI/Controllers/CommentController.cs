using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class CommentController : Controller
{
    #region Initizalize

    private readonly IMediator _mediator;
    private readonly ICodeProvider _codeProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="codeProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CommentController(IMediator mediator, ICodeProvider codeProvider)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
        this._codeProvider = Guard.GetNotNull(codeProvider, nameof(ICodeProvider));
    }

    #endregion Initizalize

    #region 创建评论

    //
    // GET: /Comment/Create/
    [Authorize]
    public async Task<IActionResult> Create(ContentVO content, MovieIdVO movieId, string transfer)
    {
        var command = new FilmHouse.Commands.Comment.CreateCommand(content, movieId);
        await this._mediator.Send(command);

        return RedirectToLocal(transfer);
    }

    #endregion

    #region 删除评论

    //
    // GET: /Comment/Delete/
    [Authorize]
    public async Task<ActionResult> Delete(CommentIdVO commentId, string transfer)
    {
        var command = new FilmHouse.Commands.Comment.DeleteCommand(commentId);
        var result = await this._mediator.Send(command);

        if (result == Commands.Comment.DeleteCommentStatus.UndefinedComment)
        {
            return RedirectToAction("NotFound", "Error");
        }
        return RedirectToLocal(transfer);
    }

    #endregion

    #region 全部评论页
    //
    // GET: /Comment/Index/
    public async Task<IActionResult> Index(MovieIdVO movieId)
    {
        var command = new FilmHouse.Commands.Comment.DisplayCommand(movieId);
        var display = await this._mediator.Send(command);
        if (display.Movie == null)
        {
            return base.RedirectToAction("NotFound", "Error");
        }

        var model = new CommentIndexViewModel();
        foreach (var comment in display.Comments)
        {
            model.Comments.Add(CommentIndexViewModel.CommentDiscViewModel.FromEntity(comment));
        }
        model.Movie = CommentIndexViewModel.MovieDiscViewModel.FromEntity(display.Movie);
        model.Movie.GenresValue = display.Movie.Genres.AsCodeElement(this._codeProvider, GenresVO.Group).Select(_ => _.Name).ToList();
        model.Movie.CountriesValue = display.Movie.Countries.AsCodeElement(this._codeProvider, CountriesVO.Group).Select(_ => _.Name).ToList();

        return View(model);
    }
    #endregion


    private ActionResult RedirectToLocal(string transfer)
    {
        if (!string.IsNullOrEmpty(transfer) && !string.IsNullOrWhiteSpace(transfer)) // !Url.IsLocalUrl(transfer) && 
        {
            return Redirect(transfer);
        }
        return RedirectToAction("Index", "Home");
    }
}