using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Album;

public record IsExistWithAlbumIdCommand(AlbumIdVO AlbumId) : IRequest<bool>;

public class IsExistWithAlbumIdCommandHandler : IRequestHandler<IsExistWithAlbumIdCommand, bool>
{
    #region Initizalize

    private readonly IRepository<AlbumEntity> _album;
    private readonly ILogger<IsExistWithAlbumIdCommandHandler> _logger;

    public IsExistWithAlbumIdCommandHandler(IRepository<AlbumEntity> album, ILogger<IsExistWithAlbumIdCommandHandler> logger)
    {
        this._album = Guard.GetNotNull(album, nameof(IRepository<AlbumEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<IsExistWithAlbumIdCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> Handle(IsExistWithAlbumIdCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AlbumIdVO, ArgumentNullException>(request.AlbumId);

        var isExist = await this._album.AnyAsync(d => d.AlbumId == request.AlbumId, ct);
        return isExist;
    }

}
