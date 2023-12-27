using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.Web.Controllers;

public class CommentController : Controller
{
    #region Initizalize

    private readonly IMediator _mediator;
    private readonly ICurrentRequestId _currentRequestId;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="currentRequestId"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CommentController(IMediator mediator, ICurrentRequestId currentRequestId)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
        this._currentRequestId = Guard.GetNotNull(currentRequestId, nameof(ICurrentRequestId));
    }

    #endregion Initizalize

    #region 创建评论

    //
    // GET: /Comment/Create/
    [Authorize]
    public async Task<IActionResult> Create(ContentVO content, MovieIdVO movieId, string transfer)
    {
        // 创建请求ID
        this._currentRequestId.Set(new RequestIdVO(Guid.NewGuid()));

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