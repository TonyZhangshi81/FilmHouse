using System.Security.Claims;
using FilmHouse.Commands.Mark;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Celeb;

public record DisplayCommand(CelebrityIdVO CelebrityId) : IRequest<DisplayContect>;

public class DisplayCommandHandler : IRequestHandler<DisplayCommand, DisplayContect>
{
    #region Initizalize

    private readonly IRepository<CelebrityEntity> _celeb;
    private readonly IRepository<MovieEntity> _movie;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<DisplayCommandHandler> _logger;
    private readonly IMediator _mediator;

    public DisplayCommandHandler(IMediator mediator, IRepository<CelebrityEntity> celeb, ILogger<DisplayCommandHandler> logger, IHttpContextAccessor httpContextAccessor, IRepository<MovieEntity> movie)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
        this._celeb = Guard.GetNotNull(celeb, nameof(IRepository<CelebrityEntity>));
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));

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
        Guard.RequiresNotNull<CelebrityIdVO, ArgumentNullException>(request.CelebrityId);

        var celeb = await this._celeb.GetAsync(request.CelebrityId, ct);

        var isCollect = false;
        var isCreate = false;

        var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
        if (userIdentity.IsAuthenticated)
        {
            // 取得登录用户的ID
            var claimsIdentity = userIdentity as ClaimsIdentity;
            var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));
            // 是否为管理员
            var isAdmin = (claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value).Equals("Administrator");

            // 是否被收藏
            isCollect = await this._mediator.Send(new IsMarkTypeCommand(new MarkTargetIdVO(request.CelebrityId.AsPrimitive()), userId, MarkTypeVO.Codes.MarkTypeCode4));

            // 是不是当前影人信息的创建者或是管理员
            isCreate = (userId == celeb.UserId || isAdmin);
        }

        IReadOnlyList<MovieEntity> discMovies = null;
        if (celeb.DoubanID != null)
        {
            var movieSpec = new MovieSpec(request.CelebrityId, 10);
            discMovies = await this._movie.SelectAsync(movieSpec, c => c, ct);
        }

        return new DisplayContect()
        {
            DiscCelebrity = celeb,
            CelebAboutMovies = discMovies,
            IsCollect = isCollect,
            IsCreate = isCreate,
        };
    }

}

public class DisplayContect
{
    public CelebrityEntity DiscCelebrity { get; set; }

    public IReadOnlyList<MovieEntity> CelebAboutMovies { get; set; }

    /// <summary>
    /// 是否被收藏
    /// </summary>
    public bool IsCollect { get; set; }
    /// <summary>
    /// 是不是当前影人信息的创建者或是管理员
    /// </summary>
    public bool IsCreate { get; set; }
}