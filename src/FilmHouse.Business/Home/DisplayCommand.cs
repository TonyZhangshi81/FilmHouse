using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Home;

public class DisplayCommand : IRequest<(int Status, IReadOnlyList<DiscoveryEntity> Discoveries)>
{
    public DisplayCommand() { }
}

public class DisplayCommandHandler : IRequestHandler<DisplayCommand, (int Status, IReadOnlyList<DiscoveryEntity> Discoveries)>
{
    #region Initizalize

    private readonly IRepository<DiscoveryEntity> _discovery;

    public DisplayCommandHandler(IRepository<DiscoveryEntity> discovery)
    {
        this._discovery = discovery;
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(int Status, IReadOnlyList<DiscoveryEntity> Discoveries)> Handle(DisplayCommand request, CancellationToken ct)
    {
        var list = await this._discovery.ListAsync(ct);
        return (0, list);
    }
}