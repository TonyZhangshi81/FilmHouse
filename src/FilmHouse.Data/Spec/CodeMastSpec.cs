using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.Spec;

public sealed class CodeMastSpec : BaseSpecification<CodeMastEntity>
{
    public CodeMastSpec() : base(c => true)
    {
        ApplyOrderBy(p => p.Group);
        ApplyOrderBy(p => p.Order);
    }

}