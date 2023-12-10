using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.Spec;

public sealed class DiscoverySpec : BaseSpecification<DiscoveryEntity>
{
    public DiscoverySpec() : base(c => true)
    {
        ApplyOrderByDescending(p => p.Order);
    }

    public DiscoverySpec(int pageSize, int pageIndex) : base(c => true)
    {
        var startRow = (pageIndex - 1) * pageSize;

        AddInclude(discovery => discovery.Include(c => c.Movie));
        ApplyOrderByDescending(p => p.Order);
        ApplyPaging(startRow, pageSize);
    }

    public DiscoverySpec(DiscoveryIdVO[] ids) : base(c => ids.Contains(c.DiscoveryId))
    {

    }

}