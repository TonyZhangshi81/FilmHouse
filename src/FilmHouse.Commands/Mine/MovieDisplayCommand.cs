using System.Security.Claims;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Mine;

/// <summary>
/// 
/// </summary>
/// <param name="Tab">页签栏（1:想看的、2:看过的、3:喜欢的、4:创建的）</param>
public record MovieDisplayCommand(int Tab) : IRequest<MineMovieDisplayContect>;


/// <summary>
/// 
/// </summary>
public class MovieDisplayCommandHandler : IRequestHandler<MovieDisplayCommand, MineMovieDisplayContect>
{
    #region Initizalize

    private readonly IRepository<UserAccountEntity> _userAccount;
    private readonly IRepository<MarkEntity> _mark;
    private readonly IRepository<MovieEntity> _movie;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<MovieDisplayCommandHandler> _logger;

    public MovieDisplayCommandHandler(IRepository<UserAccountEntity> userAccount,
                                    IRepository<MarkEntity> mark,
                                    IRepository<MovieEntity> movie,
                                    IHttpContextAccessor httpContextAccessor,
                                    ILogger<MovieDisplayCommandHandler> logger)
    {
        this._userAccount = Guard.GetNotNull(userAccount, nameof(IRepository<UserAccountEntity>));
        this._mark = Guard.GetNotNull(mark, nameof(IRepository<MarkEntity>));
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._httpContextAccessor = Guard.GetNotNull(httpContextAccessor, nameof(IHttpContextAccessor));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<MovieDisplayCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<MineMovieDisplayContect> Handle(MovieDisplayCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<int, ArgumentNullException>(request.Tab);

        // 当前登录者认证信息
        var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
        // 取得登录用户的ID
        var claimsIdentity = userIdentity as ClaimsIdentity;
        var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

        // 登录用户详细信息
        var account = await this._userAccount.GetAsync(userId, ct);

        var result = new MineMovieDisplayContect();

        // 用戶信息
        result.DiscUserAccount = account;

        // 想看的影片数量
        result.PlanCount = await this._mark.CountAsync(d => d.UserId == userId && d.Type == MarkTypeVO.Codes.MarkTypeCode1);
        // 看过的影片数量
        result.FinishCount = await this._mark.CountAsync(d => d.UserId == userId && d.Type == MarkTypeVO.Codes.MarkTypeCode2);
        // 喜欢的影片数量
        result.FavorCount = await this._mark.CountAsync(d => d.UserId == userId && d.Type == MarkTypeVO.Codes.MarkTypeCode3);
        // 自己创建的影片数量
        result.CreateCount = await this._movie.CountAsync(d => d.UserId == userId);

        // 1:想看的、2:看过的、3:喜欢的
        if (request.Tab == 1 || request.Tab == 2 || request.Tab == 3)
        {
            var marks = await this._mark.SelectAsync(new MarkSpec(new MarkTypeVO(request.Tab), userId), c => c, ct);

            foreach (var mark in marks)
            {
                var movieId = new MovieIdVO(mark.Target.AsPrimitive());
                var movie = await this._movie.GetAsync(movieId, ct);

                await this.SetCommandResultAsync(result, movie, userId, ct);
            }
        }
        // 4:创建的
        else
        {
            var movies = await this._movie.SelectAsync(new MovieSpec(userId), c => c, ct);

            foreach (var movie in movies)
            {
                await this.SetCommandResultAsync(result, movie, userId, ct);
            }
        }

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="displayContect"></param>
    /// <param name="movie"></param>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    private async Task SetCommandResultAsync(MineMovieDisplayContect displayContect, MovieEntity movie, UserIdVO userId, CancellationToken ct)
    {
        displayContect.MovieMarks.Add(new MineMovieDisplayContect.MovieMark()
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Year = movie.Year,
            Directors = movie.Directors,
            Genres = movie.Genres,
            Rating = movie.Rating,
            ReviewStatus = movie.ReviewStatus,
            Note = movie.Note,
            DoubanID = movie.DoubanID,

            // 想看
            IsPlan = await this.MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode1, ct),
            // 看过
            IsFinish = await this.MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode2, ct),
            // 喜欢
            IsFavor = await this.MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode3, ct),
        });
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


public class MineMovieDisplayContect
{
    /// <summary>
    /// 用戶信息
    /// </summary>
    public UserAccountEntity DiscUserAccount { get; set; }

    /// <summary>
    /// 想看的影片数量
    /// </summary>
    public int PlanCount { get; set; }
    /// <summary>
    /// 看过的影片数量
    /// </summary>
    public int FinishCount { get; set; }
    /// <summary>
    /// 喜欢的影片数量
    /// </summary>
    public int FavorCount { get; set; }
    /// <summary>
    /// 自己创建的影片数量
    /// </summary>
    public int CreateCount { get; set; }

    public List<MovieMark> MovieMarks { get; set; } = new List<MovieMark>();

    public class MovieMark
    {
        public MovieIdVO MovieId { get; set; }
        public MovieTitleVO Title { get; set; }
        public YearVO Year { get; set; }
        public DirectorNamesVO Directors { get; set; }
        public GenresVO Genres { get; set; }
        public RatingVO Rating { get; set; }
        public ReviewStatusVO ReviewStatus { get; set; }
        public NoteVO Note { get; set; }
        public DoubanIDVO DoubanID { get; set; }

        public bool IsPlan { get; set; }
        public bool IsFinish { get; set; }
        public bool IsFavor { get; set; }
    }

}