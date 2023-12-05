using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;

namespace FilmHouse.Business.Commands.Home;

public class DisplayCommandHandler : ICommandHandler<DisplayCommand, IReadOnlyList<DiscoveryEntity>>
{
    private readonly IRepository<DiscoveryEntity> _discovery;

    public DisplayCommandHandler(IRepository<DiscoveryEntity> discovery)
    {
        this._discovery = discovery;
    }

    public async Task<IReadOnlyList<DiscoveryEntity>> HandleAsync(DisplayCommand command)
    {
        return await this._discovery.ListAsync();
    }
}