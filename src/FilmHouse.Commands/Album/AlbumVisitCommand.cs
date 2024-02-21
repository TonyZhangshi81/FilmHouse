using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Album;

public record AlbumVisitCommand(AlbumIdVO AlbumId) : IRequest<bool>;

public class AlbumVisitCommandHandler : IRequestHandler<AlbumVisitCommand, bool>
{
    #region Initizalize

    private readonly IRepository<AlbumEntity> _album;
    private readonly ILogger<AlbumVisitCommandHandler> _logger;

    public AlbumVisitCommandHandler(IRepository<AlbumEntity> album, ILogger<AlbumVisitCommandHandler> logger)
    {
        this._album = Guard.GetNotNull(album, nameof(IRepository<AlbumEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<AlbumVisitCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AlbumVisitCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AlbumIdVO, ArgumentNullException>(request.AlbumId);

        var album = await this._album.GetAsync(request.AlbumId, ct);
        if (album.AmountAttention == null)
        {
            album.AmountAttention = 0;
        }

        // 关注度加"1"
        album.AmountAttention++;
        await this._album.UpdateAsync(album, ct);

        return true;
    }

}
