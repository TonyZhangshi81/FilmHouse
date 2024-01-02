using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Search;

public record SearchCommand(SearchKeywordVO SearchKeyword, CodeKeyVO Genre, CodeKeyVO Country, YearVO Year) : IRequest<SearchResultContect>;

public class SearchCommandHandler : IRequestHandler<SearchCommand, SearchResultContect>
{
    #region Initizalize

    private readonly ICodeProvider _codeProvider;
    private readonly IRepository<MovieEntity> _movie;
    private readonly ILogger<SearchCommandHandler> _logger;

    public SearchCommandHandler(IRepository<MovieEntity> movie, ILogger<SearchCommandHandler> logger, ICodeProvider codeProvider)
    {
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._codeProvider = Guard.GetNotNull(codeProvider, nameof(IRepository<ICodeProvider>));
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
        var query = this._movie.AsQueryable().Where(d => d.ReviewStatus == ReviewStatusVO.Codes.ReviewStatusCode2);
        // 关键字查询
        if (!string.IsNullOrEmpty(request.SearchKeyword.AsPrimitive()))
        {
            query = query.Where(d => d.Title.Contains(request.SearchKeyword.AsPrimitive()) || d.TitleEn.Contains(request.SearchKeyword.AsPrimitive()) || d.Aka.Contains(request.SearchKeyword.AsPrimitive()));
        }
        // 电影种类
        if (!"0".Equals(request.Genre.AsPrimitive()))
        {
            query = query.Where(d => d.Genres.Contains(request.Genre.AsPrimitive()));
        }
        // 国家地区
        if (!"0".Equals(request.Country.AsPrimitive()))
        {
            query = query.Where(d => d.Countries.Contains(request.Country.AsPrimitive()));
        }
        // 年代
        if (!"0".Equals(request.Year.AsPrimitive()))
        {
            query = query.Where(d => d.Year == request.Year);
        }
        query.OrderByDescending(d => d.CreatedOn);

        var list = await query.ToListAsync();
        var result = new SearchResultContect()
        {
            Movies = list.AsReadOnly()
        };
        return result;
    }
}

public class SearchResultContect
{
    public IReadOnlyList<MovieEntity> Movies { get; set; }
}