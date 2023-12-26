using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Comment;

public record DeleteCommand(CommentIdVO CommentId) : IRequest<DeleteCommentStatus>;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, DeleteCommentStatus>
{
    #region Initizalize

    private readonly IRepository<CommentEntity> _comment;
    private readonly ILogger<DeleteCommandHandler> _logger;

    public DeleteCommandHandler(IRepository<CommentEntity> comment, ILogger<DeleteCommandHandler> logger)
    {
        this._comment = Guard.GetNotNull(comment, nameof(IRepository<CommentEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<DeleteCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DeleteCommentStatus> Handle(DeleteCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<CommentIdVO, ArgumentNullException>(request.CommentId);

        if (!await this._comment.AnyAsync(d => d.CommentId == request.CommentId))
        {
            return DeleteCommentStatus.UndefinedComment;
        }

        await this._comment.DeleteAsync(request.CommentId, ct);
        return DeleteCommentStatus.Success;
    }

}

/// <summary>
/// 删除的结果
/// </summary>
public enum DeleteCommentStatus
{
    Success = 0,
    UndefinedComment = 1
}
