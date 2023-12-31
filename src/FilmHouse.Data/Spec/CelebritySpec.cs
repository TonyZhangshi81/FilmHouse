using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.Spec;

public sealed class CelebritySpec : BaseSpecification<CelebrityEntity>
{
    public CelebritySpec(CelebrityIdVO celebrityId)
        : base(c => c.CelebrityId == celebrityId)
    {
        AddInclude(celeb => celeb
        .Include(p => p.Works).ThenInclude(pc => pc.Movie));
    }

}