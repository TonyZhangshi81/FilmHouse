using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class CommentController : Controller
{
    #region Initizalize

    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CommentController(IMediator mediator)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
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




    private ActionResult RedirectToLocal(string transfer)
    {
        if (!string.IsNullOrEmpty(transfer) && !string.IsNullOrWhiteSpace(transfer)) // !Url.IsLocalUrl(transfer) && 
        {
            return Redirect(transfer);
        }
        return RedirectToAction("Index", "Home");
    }
}