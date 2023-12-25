using System.Security.Claims;
using FilmHouse.Commands.Account;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Movie;

public record DisplayCommand(MovieIdVO MovieId) : IRequest<DisplayContect>;

public class DisplayCommandHandler : IRequestHandler<DisplayCommand, DisplayContect>
{
    #region Initizalize

    private readonly IRepository<MovieEntity> _movie;
    private readonly IRepository<MarkEntity> _mark;
    private readonly IRepository<CommentEntity> _comment;
    private readonly IRepository<AlbumEntity> _album;
    private readonly ISettingProvider _settingProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<DisplayCommandHandler> _logger;

    public DisplayCommandHandler(IRepository<MovieEntity> movie, 
                                    IRepository<MarkEntity> mark, 
                                    IRepository<CommentEntity> comment, 
                                    IRepository<AlbumEntity> album, 
                                    ISettingProvider settingProvider, 
                                    IHttpContextAccessor httpContextAccessor, 
                                    ILogger<DisplayCommandHandler> logger)
    {
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._mark = Guard.GetNotNull(mark, nameof(IRepository<MarkEntity>));
        this._comment = Guard.GetNotNull(comment, nameof(IRepository<CommentEntity>));
        this._album = Guard.GetNotNull(album, nameof(IRepository<AlbumEntity>));

        this._settingProvider = Guard.GetNotNull(settingProvider, nameof(ISettingProvider));
        this._httpContextAccessor = Guard.GetNotNull(httpContextAccessor, nameof(IHttpContextAccessor));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<DisplayCommandHandler>));
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
        Guard.RequiresNotNull<MovieIdVO, ArgumentNullException>(request.MovieId);

        var movieSpec = new MovieSpec(request.MovieId, commentTake: this._settingProvider.GetValue(ConfigKeyVO.Keys.MovieCommentMax).CastTo<int>());
        var movies = await this._movie.SelectAsync(movieSpec, c => c, ct);
        if (!movies.Any())
        {
            this._logger.LogError($"MovieEntity with MovieId '{request.MovieId}' not found.");
            return null;
        }

        var movie = movies.ElementAt(0);
        // 与该影片相关的影集
        var albumSpec = new AlbumSpec(movie.MovieId);
        var albums = await this._album.SelectAsync(albumSpec, c => c, ct);

        // 评论总数
        var commentCount = await this._comment.CountAsync(d => d.MovieId == movie.MovieId, ct);

        var isPlan = false;
        var isFinish = false;
        var isFavor = false;

        var planCount = 0;
        var finishCount = 0;
        var favorCount = 0;
        // 创建者
        var isCreate = false;

        // 个人评价
        CommentEntity perComment = null;

        // 用户认证情报取得
        var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
        // 登陆后的用户可以设置对影片的偏好
        if (userIdentity.IsAuthenticated)
        {
            // 取得登录用户的ID
            var claimsIdentity = userIdentity as ClaimsIdentity;
            var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));
            var isAdmin = (claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value).Equals("Administrator");

            // 想看
            isPlan = await this.MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode1, ct);
            // 看过
            isFinish = await this.MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode2, ct);
            // 喜欢
            isFavor = await this.MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode3, ct);

            // 想看的人数
            planCount = await this._mark.CountAsync(d => d.Target == new MarkTargetIdVO(request.MovieId.AsPrimitive()) && d.Type == MarkTypeVO.Codes.MarkTypeCode1);
            // 看过的人数
            finishCount = await this._mark.CountAsync(d => d.Target == new MarkTargetIdVO(request.MovieId.AsPrimitive()) && d.Type == MarkTypeVO.Codes.MarkTypeCode2);
            // 喜欢的人数
            favorCount = await this._mark.CountAsync(d => d.Target == new MarkTargetIdVO(request.MovieId.AsPrimitive()) && d.Type == MarkTypeVO.Codes.MarkTypeCode3);

            if (movie.UserId == userId || isAdmin)
            {
                isCreate = true;
            }

            // 个人评价
            perComment = await this._comment.GetAsync(d => d.MovieId == movie.MovieId && d.UserId == userId);
        }

        return new DisplayContect()
        {
            DiscMovie = movie,
            Albums = albums,
            PersonalReview = perComment,
            CommentCount = commentCount,

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
    public IReadOnlyList<AlbumEntity> Albums { get; set; }
    public CommentEntity PersonalReview { get; set; }
    public int CommentCount { get; set; }

    public bool IsPlan { get; set; }
    public bool IsFinish { get; set; }
    public bool IsFavor { get; set; }

    public int PlanCount { get; set; }
    public int FinishCount { get; set; }
    public int FavorCount { get; set; }

    public bool IsCreate { get; set; }
}