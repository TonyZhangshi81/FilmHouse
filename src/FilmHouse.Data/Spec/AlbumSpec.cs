﻿using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;

namespace FilmHouse.Data.Spec;

public sealed class AlbumSpec : BaseSpecification<AlbumEntity>
{
    public AlbumSpec(MovieIdVO movieId)
        : base(c => c.Items.Contains(movieId.AsPrimitive().ToString()))
    {
        ApplyOrderBy(p => p.CreatedOn);
    }

}