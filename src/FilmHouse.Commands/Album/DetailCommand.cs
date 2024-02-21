using System.Security.Claims;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Album;

public record DetailCommand(AlbumIdVO AlbumId, int pagingSize, int pageIndex) : IRequest<DetailContect>;

public class DetailCommandHandler : IRequestHandler<DetailCommand, DetailContect>
{
    #region Initizalize

    private readonly IRepository<AlbumEntity> _album;
    private readonly IRepository<MarkEntity> _mark;
    private readonly IRepository<MovieEntity> _movie;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<DetailCommandHandler> _logger;

    public DetailCommandHandler(IRepository<AlbumEntity> album,
                                IRepository<MarkEntity> mark,
                                IRepository<MovieEntity> movie,
                                IHttpContextAccessor httpContextAccessor,
                                ILogger<DetailCommandHandler> logger)
    {
        this._album = Guard.GetNotNull(album, nameof(IRepository<AlbumEntity>));
        this._mark = Guard.GetNotNull(mark, nameof(IRepository<MarkEntity>));
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._httpContextAccessor = Guard.GetNotNull(httpContextAccessor, nameof(IHttpContextAccessor));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<DetailCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DetailContect> Handle(DetailCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AlbumIdVO, ArgumentNullException>(request.AlbumId);

        var album = await this._album.GetAsync(request.AlbumId, ct);

        if (album == null)
        {
            return null;
        }

        var result = new DetailContect();

        // 影集信息
        result.Album = album;

        // 用户认证情报取得
        var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
        if (userIdentity.IsAuthenticated)
        {
            // 取得登录用户的ID
            var claimsIdentity = userIdentity as ClaimsIdentity;
            var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

            // 是否为创建者
            result.IsCreate = album.UserId == userId;

            // 是否关注
            var count = await this._mark.CountAsync(d => d.Target == new MarkTargetIdVO(request.AlbumId.AsPrimitive()) && d.Type == MarkTypeVO.Codes.MarkTypeCode7);
            // 是否取消关注
            result.HasFollow = count > 0;
        }

        if (album.Items != null)
        {
            // 影集明细总件数
            var count = await this._movie.CountAsync(new MovieSpec(album.Items), ct);
            // 影集明细（单页）
            var movieSpec = new MovieSpec(album.Items, request.pagingSize, request.pageIndex);
            var list = await this._movie.SelectAsync(movieSpec, c => c, ct);

            // 总件数
            result.SearchCount = count;
            // 查询结果（单页信息）
            result.Movies = list;
        }

        return result;
    }

}


public class DetailContect
{
    /// <summary>
    /// 影集信息
    /// </summary>
    public AlbumEntity Album { get; set; }

    /// <summary>
    /// 影集明细
    /// </summary>
    public IReadOnlyList<MovieEntity> Movies { get; set; }

    /// <summary>
    /// 影集明细总件数
    /// </summary>
    public int SearchCount { get; set; }

    /// <summary>
    /// 是否取消关注
    /// </summary>
    public bool HasFollow { get; set; } = false;

    /// <summary>
    /// 是否为创建者
    /// </summary>
    public bool IsCreate { get; set; } = false;

}