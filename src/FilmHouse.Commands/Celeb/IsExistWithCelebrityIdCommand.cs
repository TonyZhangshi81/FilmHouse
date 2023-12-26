using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Celeb;

public record IsExistWithCelebrityIdCommand(CelebrityIdVO CelebrityId) : IRequest<bool>;

public class IsExistWithCelebrityIdCommandHandler : IRequestHandler<IsExistWithCelebrityIdCommand, bool>
{
    #region Initizalize

    private readonly IRepository<CelebrityEntity> _celeb;
    private readonly ILogger<IsExistWithCelebrityIdCommandHandler> _logger;

    public IsExistWithCelebrityIdCommandHandler(IRepository<CelebrityEntity> celeb, ILogger<IsExistWithCelebrityIdCommandHandler> logger)
    {
        this._celeb = Guard.GetNotNull(celeb, nameof(IRepository<CelebrityEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<IsExistWithCelebrityIdCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> Handle(IsExistWithCelebrityIdCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<CelebrityIdVO, ArgumentNullException>(request.CelebrityId);

        var isExist = await this._celeb.AnyAsync(d => d.CelebrityId == request.CelebrityId, ct);
        return isExist;
    }

}
