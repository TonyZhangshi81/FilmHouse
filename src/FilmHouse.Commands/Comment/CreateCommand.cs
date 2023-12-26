using System.Security.Claims;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Comment;

public record CreateCommand(ContentVO content, MovieIdVO movieId) : IRequest<CommentIdVO>;

public class IsExistWithAlbumIdCommandHandler : IRequestHandler<CreateCommand, CommentIdVO>
{
    #region Initizalize

    private readonly IRepository<MovieEntity> _movie;
    private readonly IRepository<CommentEntity> _comment;
    private readonly ICurrentRequestId _currentRequestId;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<IsExistWithAlbumIdCommandHandler> _logger;

    public IsExistWithAlbumIdCommandHandler(IRepository<MovieEntity> movie, IRepository<CommentEntity> comment, ILogger<IsExistWithAlbumIdCommandHandler> logger, ICurrentRequestId currentRequestId, IHttpContextAccessor httpContextAccessor)
    {
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._comment = Guard.GetNotNull(comment, nameof(IRepository<CommentEntity>));
        this._currentRequestId = Guard.GetNotNull(currentRequestId, nameof(ICurrentRequestId));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<IsExistWithAlbumIdCommandHandler>));
        this._httpContextAccessor = Guard.GetNotNull(httpContextAccessor, nameof(IHttpContextAccessor));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<CommentIdVO> Handle(CreateCommand request, CancellationToken ct)
    {
        CommentIdVO commentId = null;

        var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
        if (request.content != null && !string.IsNullOrEmpty(request.content.AsPrimitive()) && await this.IsExist(request.movieId, ct) && userIdentity.IsAuthenticated)
        {
            // 取得登录用户的ID
            var claimsIdentity = userIdentity as ClaimsIdentity;
            var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

            var comment = new CommentEntity()
            {
                RequestId = this._currentRequestId.Get(),
                CommentId = new(Guid.NewGuid()),
                Content = request.content,
                MovieId = request.movieId,
                IsEnabled = new(true),
                CommentTime = new(DateTime.Now),
                CreatedOn = new(DateTime.Now),
                UserId = userId
            };

            var result = await this._comment.AddAsync(comment);
            commentId = result.CommentId;
        }
        return commentId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movieId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async Task<bool> IsExist(MovieIdVO movieId, CancellationToken ct)
    {
        return await this._movie.AnyAsync(d => d.MovieId == movieId, ct);
    }

}
