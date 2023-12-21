using System.Security.Claims;
using System.Security.Principal;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;

namespace FilmHouse.Commands.Movie;

public record DisplayCommand(MovieIdVO MovieId, IIdentity user) : IRequest<DisplayContect>;

public class DisplayCommandHandler : IRequestHandler<DisplayCommand, DisplayContect>
{
    #region Initizalize

    private readonly IRepository<MovieEntity> _movie;
    private readonly IRepository<UserAccountEntity> _userAccount;
    private readonly IRepository<MarkEntity> _mark;
    private readonly ICurrentRequestId _currentRequestId;

    public DisplayCommandHandler(IRepository<MovieEntity> movie, IRepository<UserAccountEntity> userAccount, IRepository<MarkEntity> mark, ICurrentRequestId currentRequestId)
    {
        this._movie = movie;
        this._userAccount = userAccount;
        this._mark = mark;
        this._currentRequestId = currentRequestId;
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<DisplayContect> Handle(DisplayCommand request, CancellationToken ct)
    {
        var movieSpec = new MovieSpec(request.MovieId);
        var movies = await this._movie.SelectAsync(movieSpec, c => c, ct);
        if (!movies.Any())
        {
            return null;
        }

        var movie = movies.ElementAt(0);

        var isPlan = false;
        var isFinish = false;
        var isFavor = false;

        var planCount = 0;
        var finishCount = 0;
        var favorCount = 0;
        // 创建者
        var isCreate = false;

        // 登陆后的用户可以设置对影片的偏好
        if (request.user.IsAuthenticated)
        {
            // 取得登录用户的ID
            var claimsIdentity = request.user as ClaimsIdentity;
            var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));
            var isAdmin = (claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value).Equals("Administrator");

            // 想看
            isPlan = await MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode1, ct);
            // 看过
            isFinish = await MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode2, ct);
            // 喜欢
            isFavor = await MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode3, ct);

            // 想看的人数
            planCount = await this._mark.CountAsync(d => d.Target == new MarkTargetVO(request.MovieId.AsPrimitive()) && d.Type == MarkTypeVO.Codes.MarkTypeCode1);
            // 看过的人数
            finishCount = await this._mark.CountAsync(d => d.Target == new MarkTargetVO(request.MovieId.AsPrimitive()) && d.Type == MarkTypeVO.Codes.MarkTypeCode2);
            // 喜欢的人数
            favorCount = await this._mark.CountAsync(d => d.Target == new MarkTargetVO(request.MovieId.AsPrimitive()) && d.Type == MarkTypeVO.Codes.MarkTypeCode3);

            if (movie.UserId == userId || isAdmin)
            {
                isCreate = true;
            }
        }

        return new DisplayContect()
        {
            DiscMovie = movie,

            IsPlan = isPlan,
            IsFinish = isFinish,
            IsFavor = isFavor,

            PlanCount = planCount,
            FinishCount = finishCount,
            FavorCount = favorCount,

            IsCreate = isCreate,
        };
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
        var suit = await this._mark.AnyAsync(markSpec, ct: ct);
        return suit;
    }
}

public class DisplayContect
{
    public MovieEntity DiscMovie { get; set; }

    public bool IsPlan { get; set; }
    public bool IsFinish { get; set; }
    public bool IsFavor { get; set; }

    public int PlanCount { get; set; }
    public int FinishCount { get; set; }
    public int FavorCount { get; set; }

    public bool IsCreate { get; set; }
}