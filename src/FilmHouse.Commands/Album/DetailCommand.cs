using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Album;

public record DetailCommand(AlbumIdVO AlbumId) : IRequest<DetailContect>;

public class DetailCommandHandler : IRequestHandler<DetailCommand, DetailContect>
{
    #region Initizalize

    private readonly IRepository<AlbumEntity> _album;
    private readonly ILogger<DetailCommandHandler> _logger;

    public DetailCommandHandler(IRepository<AlbumEntity> album, ILogger<DetailCommandHandler> logger)
    {
        this._album = Guard.GetNotNull(album, nameof(IRepository<AlbumEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<DetailCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DetailContect> Handle(DetailCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AlbumIdVO, ArgumentNullException>(request.AlbumId);

        var isExist = await this._album.AnyAsync(d => d.AlbumId == request.AlbumId, ct);
        return null;
    }

}


public class DetailContect
{
    public AlbumEntity Album { get; set; }
}