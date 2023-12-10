using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using System.Linq;
using MediatR;
using FilmHouse.Data.Spec;
using System;
using System.Security.Principal;

namespace FilmHouse.Commands.Home;

public record DisplayCommand(int pageIndex, int maxIndex, IIdentity user) : IRequest<DisplayContect>;

public class DisplayCommandHandler : IRequestHandler<DisplayCommand, DisplayContect>
{
    #region Initizalize

    private readonly IRepository<DiscoveryEntity> _discovery;
    private readonly IRepository<MovieEntity> _movie;
    private readonly IRepository<MarkEntity> _mark;
    private readonly IRepository<UserAccountEntity> _userAccount;

    public DisplayCommandHandler(IRepository<DiscoveryEntity> discovery, IRepository<MovieEntity> movie, IRepository<MarkEntity> mark, IRepository<UserAccountEntity> userAccount)
    {
        this._discovery = discovery;
        this._movie = movie;
        this._mark = mark;
        this._userAccount = userAccount;
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<DisplayContect> Handle(DisplayCommand request, CancellationToken ct)
    {
        var pageIndex = (request.pageIndex <= 0) ? 1 : request.pageIndex;

        // 表中没有记录
        if (!await this._discovery.AnyAsync(ct:ct))
        {
            return new DisplayContect() { Status = 0, CurrentPageIndex = 0 };
        }

        // 现有记录数
        var allCount = await this._discovery.CountAsync(ct: ct);
        // 从首页至最后一页
        if (pageIndex == -1)
        {
            // 现有记录数大于等于设定页数的时候，就取设置页数上的信息
            if(allCount >= request.maxIndex)
            {
                pageIndex = request.maxIndex;
            }
            else
            {
                pageIndex = allCount;
            }
        }
        else
        {
            // 现有记录数小于翻页书的时候，就取现有记录数上的信息
            if (allCount < pageIndex)
            {
                pageIndex = allCount;
            }
        }

        // 每日发现
        var discoverySpec = new DiscoverySpec(1, pageIndex);
        var discoveryQuery = await this._discovery.SelectAsync(discoverySpec, c => c, ct);
        var discMovie = discoveryQuery[0].Movie;
        // 最新栏目
        var newMoviesSpec = new MovieSpec(20, 1, ReviewStatusVO.Codes.ReviewStatusCode1);
        var newMovies = await this._movie.SelectAsync(newMoviesSpec, c => c, ct);
        // 热门栏目
        var mostMoviesSpec = new MovieSpec(20, 1, ReviewStatusVO.Codes.ReviewStatusCode2);
        var mostMovies = await this._movie.SelectAsync(mostMoviesSpec, c => c, ct);

        var markTargetId = new MarkTargetVO(discoveryQuery[0].MovieId.AsPrimitive());

        var isPlan = false;
        var isFinish = false;
        var isFavor = false;

        // 登陆后的用户可以设置对影片的偏好
        if (request.user.IsAuthenticated)
        {
            var accountSpec = new UserAccountSpec(new AccountNameVO(request.user.Name));
            // 取得登录用户的ID
            var userId = await this._userAccount.FirstOrDefaultAsync(accountSpec, c => c.UserId);

            // 想看
            isPlan = await MarkCheckAsync(markTargetId, userId, MarkTypeVO.Codes.MarkTypeCode1, ct: ct);
            // 看过
            isFinish = await MarkCheckAsync(markTargetId, userId, MarkTypeVO.Codes.MarkTypeCode2, ct: ct);
            // 喜欢
            isFavor = await MarkCheckAsync(markTargetId, userId, MarkTypeVO.Codes.MarkTypeCode3, ct: ct);
        }

        return new DisplayContect()
        {
            Status = 0,
            CurrentPageIndex = pageIndex,
            Discoveries = discoveryQuery,
            DiscMovie = discMovie,
            NewMovies = newMovies,
            MostMovies = mostMovies,
            IsPlan = isPlan,
            IsFinish = isFinish,
            IsFavor = isFavor,
        };
    }

    /// <summary>
    /// 检查是否已经标记过
    /// </summary>
    /// <param name="tagret">标记对象id</param>
    /// <param name="userId">用户id</param>
    /// <param name="type">标记类型</param>
    /// <returns>标记过true，否则false</returns>
    private async Task<bool> MarkCheckAsync(MarkTargetVO target, UserIdVO userId, MarkTypeVO type, CancellationToken ct)
    {
        var markSpec = new MarkSpec(type, userId, target);
        var suit = await this._mark.AnyAsync(markSpec, ct: ct);
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
    public IReadOnlyList<DiscoveryEntity> Discoveries { get; set; }
    public MovieEntity DiscMovie { get; set; }
    public IReadOnlyList<MovieEntity> NewMovies { get; set; }
    public IReadOnlyList<MovieEntity> MostMovies { get; set; }

    public bool IsPlan { get; set; }
    public bool IsFinish { get; set; }
    public bool IsFavor { get; set; }
}