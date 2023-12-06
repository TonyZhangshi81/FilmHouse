using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;

namespace FilmHouse.Business.Commands.Home;

public class DisplayCommand
{
    public DisplayCommand()
    {
    }
}

public class DisplayCommandHandler : ICommandHandler<DisplayCommand, (int status, IReadOnlyList<DiscoveryEntity> discoveries)>
{
    private readonly IRepository<DiscoveryEntity> _discovery;

    public DisplayCommandHandler(IRepository<DiscoveryEntity> discovery)
    {
        this._discovery = discovery;
    }

    public async Task<(int status, IReadOnlyList<DiscoveryEntity> discoveries)> HandleAsync(DisplayCommand command)
    {
        var list = await this._discovery.ListAsync();
        return (0, list);
    }
}