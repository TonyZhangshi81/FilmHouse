using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.Spec;

public sealed class MovieSpec : BaseSpecification<MovieEntity>
{
    public MovieSpec(int pageSize, int pageIndex, ReviewStatusVO reviewStatus) 
        : base(c => c.ReviewStatus == reviewStatus)
    {
        var startRow = (pageIndex - 1) * pageSize;

        // 最新栏目
        if (reviewStatus == ReviewStatusVO.Codes.ReviewStatusCode1)
        {
            ApplyOrderByDescending(p => p.CreatedOn);
        }

        // 热门栏目
        if (reviewStatus == ReviewStatusVO.Codes.ReviewStatusCode2)
        {
            ApplyOrderByDescending(p => p.PageViews);
        }

        ApplyPaging(startRow, pageSize);
    }

    public MovieSpec(MovieIdVO[] ids) 
        : base(c => ids.Contains(c.MovieId))
    {
    }

    public MovieSpec(MovieIdVO id) 
        : base(c => id == c.MovieId)
    {
        AddInclude(movie => movie
        .Include(p => p.Asks)
        .Include(p => p.Comments)
        .Include(p => p.Resources).ThenInclude(pc => pc.UserAccount));
    }
}