using System.IO;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FilmHouse.Core.Utils;

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

    public MovieSpec(CelebrityIdVO celebrityId, int pageSize)
        : base(c => c.DirectorsId.Contains(celebrityId.AsPrimitive().ToString()) 
                    || c.WritersId.Contains(celebrityId.AsPrimitive().ToString())
                    || c.CastsId.Contains(celebrityId.AsPrimitive().ToString()))
    {
        ApplyOrderByDescending(p => p.Rating);
        ApplyPaging(0, pageSize);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 1.规定评论依照时间降序显示
    /// 2.影片资源文件只显示已通过评审状态的文件
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="commentTake"></param>
    public MovieSpec(MovieIdVO id, int commentTake)
        : base(c => id == c.MovieId)
    {
        AddInclude(movie => movie
        .Include(p => p.Asks)
        .Include(p => p.Comments.OrderByDescending(d => d.CommentTime).Take(commentTake)).ThenInclude(pc => pc.UserAccount)
        .Include(p => p.Resources.Where(d => d.ReviewStatus == ReviewStatusVO.Codes.ReviewStatusCode2).OrderByDescending(d => d.CreatedOn)).ThenInclude(pc => pc.UserAccount)
        );
    }
}