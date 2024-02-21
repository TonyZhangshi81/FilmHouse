using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.Spec;

public sealed class AlbumSpec : BaseSpecification<AlbumEntity>
{
    public AlbumSpec() : base()
    {
        AddInclude(album => album.Include(p => p.UserAccount));
        ApplyOrderByDescending(p => p.AmountAttention);
    }

    public AlbumSpec(UserIdVO userId) : base(c => c.UserId == userId)
    {
        AddInclude(album => album.Include(p => p.UserAccount));
    }

    public AlbumSpec(MovieIdVO movieId) : base(c => c.Items.Contains(movieId.AsPrimitive().ToString()))
    {
        AddInclude(album => album.Include(p => p.UserAccount));
        ApplyOrderBy(p => p.CreatedOn);
    }
}