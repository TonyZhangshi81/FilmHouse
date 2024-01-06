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

public record InitCommand() : IRequest<DisplayContect>;

public class InitCommandHandler : IRequestHandler<InitCommand, DisplayContect>
{
    #region Initizalize

    private readonly ICodeProvider _codeProvider;
    private readonly IRepository<MovieEntity> _movie;
    private readonly ILogger<InitCommandHandler> _logger;

    public InitCommandHandler(IRepository<MovieEntity> movie, ILogger<InitCommandHandler> logger, ICodeProvider codeProvider)
    {
        this._movie = Guard.GetNotNull(movie, nameof(IRepository<MovieEntity>));
        this._codeProvider = Guard.GetNotNull(codeProvider, nameof(IRepository<ICodeProvider>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<InitCommandHandler>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DisplayContect> Handle(InitCommand request, CancellationToken ct)
    {
        var listGenre = new List<SelectListItem>();
        var listCountry = new List<SelectListItem>();
        var listYear = new List<SelectListItem>();

        // 电影种类
        var genres = this._codeProvider.AvailableAt(new CodeGroupVO(CodeGroupVO.Codes.MovieGenre.AsPrimitive())).Elements;
        foreach (var item in genres)
        {
            listGenre.Add(new SelectListItem() { Text = item.Name.AsPrimitive(), Value = item.Code.AsPrimitive() });
        }
        listGenre.Insert(0, new SelectListItem() { Text = "选择类型", Value = "0" });

        // 国家地区
        var countries = this._codeProvider.AvailableAt(new CodeGroupVO(CodeGroupVO.Codes.Country.AsPrimitive())).Elements;
        foreach (var item in countries)
        {
            listCountry.Add(new SelectListItem() { Text = item.Name.AsPrimitive(), Value = item.Code.AsPrimitive() });
        }
        listCountry.Insert(0, new SelectListItem() { Text = "选择国家/地区", Value = "0" });

        // 年代
        var years = await this._movie.AsQueryable().Where(d => d.Year != null).Select(d => d.Year).Distinct().OrderByDescending(y => y).ToListAsync();
        foreach (var item in years)
        {
            listYear.Add(new SelectListItem() {Text = item.AsPrimitive(), Value = item.AsPrimitive() });
        }
        listYear.Insert(0, new SelectListItem() { Text = "选择年代", Value = "0" });

        var result = new DisplayContect()
        {
            ListGenre = listGenre.AsReadOnly(),
            ListCountry = listCountry.AsReadOnly(),
            ListYear = listYear.AsReadOnly(),
        };
        return result;
    }

}

public class DisplayContect
{
    public IReadOnlyList<SelectListItem> ListGenre { get; set; }
    public IReadOnlyList<SelectListItem> ListCountry { get; set; }
    public IReadOnlyList<SelectListItem> ListYear { get; set; }
}