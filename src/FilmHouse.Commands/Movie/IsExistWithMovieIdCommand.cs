using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Movie;

public record IsExistWithMovieIdCommand(MovieIdVO MovieId) : IRequest<bool>;

public class IsExistWithMovieIdCommandHandler : IRequestHandler<IsExistWithMovieIdCommand, bool>
{
    #region Initizalize

    private readonly IRepository<MovieEntity> _movie;
    private readonly ILogger<IsExistWithMovieIdCommandHandler> _logger;

    public IsExistWithMovieIdCommandHandler(IRepository<MovieEntity> movie, ILogger<IsExistWithMovieIdCommandHandler> logger)
    {
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<IsExistWithMovieIdCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> Handle(IsExistWithMovieIdCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<MovieIdVO, ArgumentNullException>(request.MovieId);

        var isExist = await this._movie.AnyAsync(d => d.MovieId == request.MovieId, ct);
        return isExist;
    }

}
