using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Ask;

public record IsExistWithAskIdCommand(AskIdVO AskId) : IRequest<bool>;

public class IsExistWithAskIdCommandHandler : IRequestHandler<IsExistWithAskIdCommand, bool>
{
    #region Initizalize

    private readonly IRepository<AskEntity> _ask;
    private readonly ILogger<IsExistWithAskIdCommandHandler> _logger;

    public IsExistWithAskIdCommandHandler(IRepository<AskEntity> ask, ILogger<IsExistWithAskIdCommandHandler> logger)
    {
        this._ask = Guard.GetNotNull(ask, nameof(IRepository<AskEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<IsExistWithAskIdCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> Handle(IsExistWithAskIdCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AskIdVO, ArgumentNullException>(request.AskId);

        var isExist = await this._ask.AnyAsync(d => d.AskId == request.AskId, ct);
        return isExist;
    }

}
