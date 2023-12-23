using System.Security.Claims;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FilmHouse.Commands.Home;

public record DisplayCommand(int pageIndex, int maxIndex) : IRequest<DisplayContect>;

public class DisplayCommandHandler : IRequestHandler<DisplayCommand, DisplayContect>
{
    #region Initizalize

    private readonly IRepository<DiscoveryEntity> _discovery;
    private readonly IRepository<MovieEntity> _movie;
    private readonly IRepository<MarkEntity> _mark;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DisplayCommandHandler(IRepository<DiscoveryEntity> discovery, IRepository<MovieEntity> movie, IRepository<MarkEntity> mark, IHttpContextAccessor httpContextAccessor)
    {
        this._discovery = Guard.GetNotNull(discovery, nameof(DiscoveryEntity));
        this._movie = Guard.GetNotNull(movie, nameof(MovieEntity));
        this._mark = Guard.GetNotNull(mark, nameof(MarkEntity));

        this._httpContextAccessor = Guard.GetNotNull(httpContextAccessor, nameof(IHttpContextAccessor));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DisplayContect> Handle(DisplayCommand request, CancellationToken ct)
    {
        // 表中没有记录
        if (!await this._discovery.AnyAsync(ct: ct))
        {
            return new DisplayContect() { Status = 0, CurrentPageIndex = 0 };
        }

        var currentPageIndex = request.pageIndex;
        var (prePageIndex, postPageIndex) = await this.CalcPagination(currentPageIndex, request.maxIndex, ct);

        // 每日发现
        var discoverySpec = new DiscoverySpec(1, currentPageIndex);
        var discoveryQuery = await this._discovery.SelectAsync(discoverySpec, c => c, ct);
        var discMovie = discoveryQuery[0].Movie;
        // 最新栏目
        var newMoviesSpec = new MovieSpec(20, 1, ReviewStatusVO.Codes.ReviewStatusCode1);
        var newMovies = await this._movie.SelectAsync(newMoviesSpec, c => c, ct);
        // 热门栏目
        var mostMoviesSpec = new MovieSpec(20, 1, ReviewStatusVO.Codes.ReviewStatusCode2);
        var mostMovies = await this._movie.SelectAsync(mostMoviesSpec, c => c, ct);

        var movieId = discoveryQuery[0].MovieId;

        var isPlan = false;
        var isFinish = false;
        var isFavor = false;

        var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
        // 登陆后的用户可以设置对影片的偏好
        if (userIdentity.IsAuthenticated)
        {
            // 取得登录用户的ID
            var claimsIdentity = userIdentity as ClaimsIdentity;
            var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

            // 想看
            isPlan = await MarkCheckAsync(movieId, userId, MarkTypeVO.Codes.MarkTypeCode1, ct);
            // 看过
            isFinish = await MarkCheckAsync(movieId, userId, MarkTypeVO.Codes.MarkTypeCode2, ct);
            // 喜欢
            isFavor = await MarkCheckAsync(movieId, userId, MarkTypeVO.Codes.MarkTypeCode3, ct);
        }

        return new DisplayContect()
        {
            Status = 0,

            Discoveries = discoveryQuery,
            DiscMovie = discMovie,
            NewMovies = newMovies,
            MostMovies = mostMovies,

            CurrentPageIndex = currentPageIndex,
            PrePageIndex = prePageIndex,
            PostPageIndex = postPageIndex,

            IsPlan = isPlan,
            IsFinish = isFinish,
            IsFavor = isFavor,
        };
    }

    private async Task<Tuple<int, int>> CalcPagination(int currentPageIndex, int maxIndex, CancellationToken ct)
    {
        // 上一页
        var prePageIndex = currentPageIndex - 1;
        // 下一页
        var postPageIndex = currentPageIndex + 1;

        // 现有记录数
        var allCount = await this._discovery.CountAsync(ct: ct);

        // 从首页至最后一页
        if (prePageIndex == 0)
        {
            // 确定最后的页码
            prePageIndex = (allCount >= maxIndex) ? maxIndex : allCount;
        }

        // 从最后一页至首页
        if (postPageIndex > maxIndex || postPageIndex > allCount)
        {
            postPageIndex = 1;
        }

        return Tuple.Create(prePageIndex, postPageIndex);
    }

    /// <summary>
    /// 检查是否已经标记过
    /// </summary>
    /// <param name="movieId">标记对象id</param>
    /// <param name="userId">用户id</param>
    /// <param name="type">标记类型</param>
    /// <returns>标记过true，否则false</returns>
    private async Task<bool> MarkCheckAsync(MovieIdVO movieId, UserIdVO userId, MarkTypeVO type, CancellationToken ct)
    {
        var markSpec = new MarkSpec(type, userId, target: new(movieId.AsPrimitive()));
        var suit = await this._mark.AnyAsync(markSpec, ct);
        return suit;
    }
}

public class DisplayContect
{
    /// <summary>
    /// 0:正常; 1:异常
    /// </summary>
    public int Status { get; set; }

    public int CurrentPageIndex { get; set; }
    public int PostPageIndex { get; set; }
    public int PrePageIndex { get; set; }

    public IReadOnlyList<DiscoveryEntity> Discoveries { get; set; }
    public MovieEntity DiscMovie { get; set; }
    public IReadOnlyList<MovieEntity> NewMovies { get; set; }
    public IReadOnlyList<MovieEntity> MostMovies { get; set; }

    public bool IsPlan { get; set; }
    public bool IsFinish { get; set; }
    public bool IsFavor { get; set; }
}