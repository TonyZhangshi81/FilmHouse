using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Resource;

public record IsExistWithResourceIdCommand(ResourceIdVO ResourceId) : IRequest<bool>;

public class IsExistWithResourceIdCommandHandler : IRequestHandler<IsExistWithResourceIdCommand, bool>
{
    #region Initizalize

    private readonly IRepository<ResourceEntity> _resource;
    private readonly ILogger<IsExistWithResourceIdCommandHandler> _logger;

    public IsExistWithResourceIdCommandHandler(IRepository<ResourceEntity> resource, ILogger<IsExistWithResourceIdCommandHandler> logger)
    {
        this._resource = Guard.GetNotNull(resource, nameof(IRepository<ResourceEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<IsExistWithResourceIdCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> Handle(IsExistWithResourceIdCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<ResourceIdVO, ArgumentNullException>(request.ResourceId);

        var isExist = await this._resource.AnyAsync(d => d.ResourceId == request.ResourceId, ct);
        return isExist;
    }

}
