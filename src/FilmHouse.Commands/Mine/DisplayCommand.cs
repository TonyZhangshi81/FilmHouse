using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Mine;

public record DisplayCommand(UserIdVO UserId) : IRequest<DisplayContect>;


public class DisplayCommandHandler : IRequestHandler<DisplayCommand, DisplayContect>
{
    #region Initizalize

    private readonly IRepository<UserAccountEntity> _userAccount;
    private readonly IRepository<MarkEntity> _mark;
    private readonly IRepository<MovieEntity> _movie;
    private readonly IRepository<CelebrityEntity> _celebrity;
    private readonly IRepository<AlbumEntity> _album;
    private readonly IRepository<CommentEntity> _comment;

    private readonly ILogger<DisplayCommandHandler> _logger;

    public DisplayCommandHandler(IRepository<UserAccountEntity> userAccount,
                                    IRepository<MarkEntity> mark,
                                    IRepository<MovieEntity> movie,
                                    IRepository<CelebrityEntity> celebrity,
                                    IRepository<AlbumEntity> album,
                                    IRepository<CommentEntity> comment,
                                    ILogger<DisplayCommandHandler> logger)
    {
        this._userAccount = Guard.GetNotNull(userAccount, nameof(IRepository<UserAccountEntity>));
        this._mark = Guard.GetNotNull(mark, nameof(IRepository<MarkEntity>));
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._celebrity = Guard.GetNotNull(celebrity, nameof(IRepository<CelebrityEntity>));
        this._album = Guard.GetNotNull(album, nameof(IRepository<AlbumEntity>));
        this._comment = Guard.GetNotNull(comment, nameof(IRepository<CommentEntity>));

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
        Guard.RequiresNotNull<UserIdVO, ArgumentNullException>(request.UserId);

        var account = await this._userAccount.GetAsync(request.UserId, ct);
        if (account == null)
        {
            return new DisplayContect();
        }

        var result = new DisplayContect();
        // 用戶信息
        result.DiscUserAccount = account;

        // 想看的电影
        var moviePlans = await this.GetMoviePlansAsync(request.UserId, ct);
        result.MoviePlans = moviePlans.AsReadOnly();

        // 看过的电影
        var movieFinishs = await this.GetMovieFinishsAsync(request.UserId, ct);
        result.MovieFinishs = movieFinishs.AsReadOnly();

        // 喜欢的电影
        var movieFavors = await this.GetMovieFavorsAsync(request.UserId, ct);
        result.MovieFavors = movieFavors.AsReadOnly();

        // 收藏的影人
        var celebCollects = await this.GetCelebCollectsAsync(request.UserId, ct);
        result.CelebCollects = celebCollects.AsReadOnly();

        // 专辑
        result.Albums = await this._album.SelectAsync(new AlbumSpec(request.UserId), c => c, ct);
        result.AlbumFollows = await this.GetAlbumFollowsAsync(result.Albums, ct);

        // 评论
        result.Comments = await this._comment.SelectAsync(new CommentSpec(request.UserId), c => c, ct);

        return result;
    }

    /// <summary>
    /// 共同喜好的电影
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="loginUserId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async Task<List<MovieEntity>> GetMovieCommonsAsync(UserIdVO userId, UserIdVO loginUserId, CancellationToken ct)
    {
        var movieCommons = new List<MovieEntity>();
        // 当前专辑作者所喜欢的电影信息
        var movieTargets = await this._mark.SelectAsync(d => d.UserId == userId && d.Type == MarkTypeVO.Codes.MarkTypeCode3, c => c.Target, ct);
        foreach (var target in movieTargets)
        {
            // 与当前登录者有共同喜欢的电影信息
            if (await this._mark.AnyAsync(d => d.Target == target && d.UserId == loginUserId && d.Type == MarkTypeVO.Codes.MarkTypeCode3, ct))
            {
                movieCommons.Add(await this._movie.GetAsync(new MovieIdVO(target.AsPrimitive()), ct));
            }
        }
        return movieCommons;
    }

    /// <summary>
    /// 想看的电影
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async Task<List<MovieEntity>> GetMoviePlansAsync(UserIdVO userId, CancellationToken ct)
    {
        var moviePlans = new List<MovieEntity>();
        var movieTargets = await this._mark.SelectAsync(d => d.UserId == userId && d.Type == MarkTypeVO.Codes.MarkTypeCode1, c => c.Target, ct);
        foreach (var target in movieTargets)
        {
            moviePlans.Add(await this._movie.GetAsync(new MovieIdVO(target.AsPrimitive()), ct));
        }
        return moviePlans;
    }

    /// <summary>
    /// 看过的电影
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async Task<List<MovieEntity>> GetMovieFinishsAsync(UserIdVO userId, CancellationToken ct)
    {
        var movieFinishs = new List<MovieEntity>();
        var movieTargets = await this._mark.SelectAsync(d => d.UserId == userId && d.Type == MarkTypeVO.Codes.MarkTypeCode2, c => c.Target, ct);
        foreach (var target in movieTargets)
        {
            movieFinishs.Add(await this._movie.GetAsync(new MovieIdVO(target.AsPrimitive()), ct));
        }
        return movieFinishs;
    }

    /// <summary>
    /// 喜欢的电影
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async Task<List<MovieEntity>> GetMovieFavorsAsync(UserIdVO userId, CancellationToken ct)
    {
        var movieFavors = new List<MovieEntity>();
        var movieTargets = await this._mark.SelectAsync(d => d.UserId == userId && d.Type == MarkTypeVO.Codes.MarkTypeCode3, c => c.Target, ct);
        foreach (var target in movieTargets)
        {
            movieFavors.Add(await this._movie.GetAsync(new MovieIdVO(target.AsPrimitive()), ct));
        }
        return movieFavors;
    }

    /// <summary>
    /// 收藏的影人
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async Task<List<CelebrityEntity>> GetCelebCollectsAsync(UserIdVO userId, CancellationToken ct)
    {
        var celebrities = new List<CelebrityEntity>();
        var movieTargets = await this._mark.SelectAsync(d => d.UserId == userId && d.Type == MarkTypeVO.Codes.MarkTypeCode4, c => c.Target, ct);
        foreach (var target in movieTargets)
        {
            celebrities.Add(await this._celebrity.GetAsync(new CelebrityIdVO(target.AsPrimitive()), ct));
        }
        return celebrities;
    }


    /// <summary>
    /// 專輯收藏情況
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async Task<List<DisplayContect.AlbumFollow>> GetAlbumFollowsAsync(IReadOnlyList<AlbumEntity> albums, CancellationToken ct)
    {
        var albumFollows = new List<DisplayContect.AlbumFollow>();
        foreach (var item in albums)
        {
            var count = await this._mark.CountAsync(d => d.Target == new MarkTargetIdVO(item.AlbumId.AsPrimitive()) && d.Type == MarkTypeVO.Codes.MarkTypeCode7, ct);
            albumFollows.Add(new DisplayContect.AlbumFollow()
            {
                AlbumId = item.AlbumId,
                FollowCount = new FollowCountVO(count)
            });
        }
        return albumFollows;
    }

}



public class DisplayContect
{
    /// <summary>
    /// 用戶信息
    /// </summary>
    public UserAccountEntity DiscUserAccount { get; set; }

    /// <summary>
    /// 想看的电影
    /// </summary>
    public IReadOnlyList<MovieEntity> MoviePlans { get; set; }

    /// <summary>
    /// 看过的电影
    /// </summary>
    public IReadOnlyList<MovieEntity> MovieFinishs { get; set; }

    /// <summary>
    /// 共同喜好的电影
    /// </summary>
    public IReadOnlyList<MovieEntity> MovieCommons { get; set; }

    /// <summary>
    /// 喜欢的电影
    /// </summary>
    public IReadOnlyList<MovieEntity> MovieFavors { get; set; }

    /// <summary>
    /// 收藏的影人
    /// </summary>
    public IReadOnlyList<CelebrityEntity> CelebCollects { get; set; }

    /// <summary>
    /// 专辑
    /// </summary>
    public IReadOnlyList<AlbumEntity> Albums { get; set; }
    public IReadOnlyList<AlbumFollow> AlbumFollows { get; set; }
    /// <summary>
    /// 專輯收藏情況
    /// </summary>
    public class AlbumFollow
    {
        public AlbumIdVO AlbumId { get; set; }
        // 專輯被收藏的數量
        public FollowCountVO FollowCount { get; set; } = 0;
    }

    /// <summary>
    /// 评论
    /// </summary>
    public IReadOnlyList<CommentEntity> Comments { get; set; }
}