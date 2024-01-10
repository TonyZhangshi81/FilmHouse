using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.Spec;

public sealed class CommentSpec : BaseSpecification<CommentEntity>
{
    public CommentSpec(MovieIdVO movieId)
        : base(c => c.MovieId == movieId)
    {
        AddInclude(comment => comment.Include(p => p.UserAccount));
        ApplyOrderByDescending(p => p.CommentTime);
    }

}