using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Album;

public record AddCommand(AlbumIdVO AlbumId, MovieIdVO MovieId) : IRequest<bool>;

public class AddCommandHandler : IRequestHandler<AddCommand, bool>
{
    #region Initizalize

    private readonly IRepository<AlbumEntity> _album;
    private readonly IRepository<MovieEntity> _movie;
    private readonly ILogger<AddCommandHandler> _logger;

    public AddCommandHandler(IRepository<AlbumEntity> album, IRepository<MovieEntity> movie, ILogger<AddCommandHandler> logger)
    {
        this._album = Guard.GetNotNull(album, nameof(IRepository<AlbumEntity>));
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<AddCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AlbumIdVO, ArgumentNullException>(request.AlbumId);

        var album = await this._album.GetAsync(request.AlbumId, ct);
        if (album == null)
        {
            return false;
        }

        var movie = await this._movie.GetAsync(request.MovieId, ct);
        if (movie == null)
        {
            return false;
        }

        // 添加影片至影集
        if (album.Items == null)
        {
            album.Items = new AlbumJsonItemsVO($"{movie.MovieId.ToString()}");
        }
        else
        {
            album.Items = new AlbumJsonItemsVO($"{album.Items.AsPrimitive()},{movie.MovieId.ToString()}");
        }


        await this._album.UpdateAsync(album, ct);

        return true;
    }

}
