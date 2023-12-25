using System;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.Spec;

public sealed class MarkSpec : BaseSpecification<MarkEntity>
{
    public MarkSpec(MarkTypeVO markType, UserIdVO userId, MarkTargetIdVO target) 
        : base(c => c.Type == markType && c.UserId == userId && c.Target == target)
    {
    }



}