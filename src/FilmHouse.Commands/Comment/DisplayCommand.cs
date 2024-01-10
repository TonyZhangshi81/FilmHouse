using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Comment;

public record DisplayCommand(MovieIdVO MovieId) : IRequest<DisplayContect>;

public class DisplayCommandHandler : IRequestHandler<DisplayCommand, DisplayContect>
{
    #region Initizalize

    private readonly IRepository<MovieEntity> _movie;
    private readonly IRepository<CommentEntity> _comment;
    private readonly ILogger<DisplayCommandHandler> _logger;

    public DisplayCommandHandler(
        IRepository<MovieEntity> movie,
        IRepository<CommentEntity> comment,
        ILogger<DisplayCommandHandler> logger)
    {
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._comment = Guard.GetNotNull(comment, nameof(IRepository<CommentEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<DisplayCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DisplayContect> Handle(DisplayCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<MovieIdVO, ArgumentNullException>(request.MovieId);

        var movie = await this._movie.GetAsync(request.MovieId, ct);
        if (movie == null)
        {
            return new DisplayContect();
        }

        var result = new DisplayContect();
        // 影片信息
        result.Movie = movie;

        var commentSpec = new CommentSpec(request.MovieId);
        var comments = await this._comment.SelectAsync(commentSpec, c => c, ct);
        // 評論信息
        result.Comments = comments;

        return result;
    }
}

public class DisplayContect
{
    public MovieEntity Movie { get; set; }
    public IReadOnlyList<CommentEntity> Comments { get; set; }
}