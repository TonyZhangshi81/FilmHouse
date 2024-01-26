using System.Security.Claims;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Search;

public record SearchCommand(SearchKeywordVO SearchKeyword, CodeKeyVO Genre, CodeKeyVO Country, YearVO Year, int pagingSize, int pageIndex) : IRequest<SearchResultContect>;

public class SearchCommandHandler : IRequestHandler<SearchCommand, SearchResultContect>
{
    #region Initizalize

    private readonly ICodeProvider _codeProvider;
    private readonly IRepository<MovieEntity> _movie;
    private readonly IRepository<MarkEntity> _mark;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SearchCommandHandler> _logger;

    public SearchCommandHandler(IRepository<MovieEntity> movie, IRepository<MarkEntity> mark, ILogger<SearchCommandHandler> logger, ICodeProvider codeProvider, IHttpContextAccessor httpContextAccessor)
    {
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._mark = Guard.GetNotNull(mark, nameof(IRepository<MarkEntity>));
        this._codeProvider = Guard.GetNotNull(codeProvider, nameof(IRepository<ICodeProvider>));
        this._httpContextAccessor = Guard.GetNotNull(httpContextAccessor, nameof(IHttpContextAccessor));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<SearchCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<SearchResultContect> Handle(SearchCommand request, CancellationToken ct)
    {
        var count = await this._movie.CountAsync(new MovieSpec(request.SearchKeyword, request.Genre, request.Country, request.Year, ReviewStatusVO.Codes.ReviewStatusCode2), ct);

        var movieSpec = new MovieSpec(request.SearchKeyword, request.Genre, request.Country, request.Year, ReviewStatusVO.Codes.ReviewStatusCode2, request.pagingSize, request.pageIndex);

        var list = await this._movie.SelectAsync(movieSpec, c => c, ct);
        var result = new SearchResultContect()
        {
            // 总件数
            SearchCount = count,
            // 查询结果（单页信息）
            Movies = list
        };


        // 用户认证情报取得
        var userIdentity = this._httpContextAccessor.HttpContext.User.Identity;
        // 登陆后的用户可以设置对影片的偏好
        if (userIdentity.IsAuthenticated)
        {
            // 取得登录用户的ID
            var claimsIdentity = userIdentity as ClaimsIdentity;
            var userId = new UserIdVO(new Guid(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid").Value));

            foreach (var movie in list)
            {
                result.Marks.Add(new SearchResultContect.Mark()
                {
                    MovieId = movie.MovieId,
                    // 想看
                    IsPlan = await this.MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode1, ct),
                    // 看过
                    IsFinish = await this.MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode2, ct),
                    // 喜欢
                    IsFavor = await this.MarkCheckAsync(movie.MovieId, userId, MarkTypeVO.Codes.MarkTypeCode3, ct),
                });
            }
        }


        return result;
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

public class SearchResultContect
{
    public IReadOnlyList<MovieEntity> Movies { get; set; }

    public int SearchCount { get; set; }

    public List<Mark> Marks { get; set; } = new List<Mark>();

    public class Mark
    {
        public MovieIdVO MovieId { get; set; }

        public bool IsPlan { get; set; }
        public bool IsFinish { get; set; }
        public bool IsFavor { get; set; }

    }
}