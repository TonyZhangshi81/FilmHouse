﻿using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.App.Presentation.Web.UI.Models;

public class MovieViewModel
{
    public MovieDiscViewModel Movie { get; set; } = new MovieDiscViewModel();

    public List<ResourceDiscViewModel> Resources { get; set; } = new List<ResourceDiscViewModel>();

    public List<CommentDiscViewModel> Comments { get; set; } = new List<CommentDiscViewModel>();

    public List<AlbumDiscViewModel> Albums { get; set; } = new List<AlbumDiscViewModel>();

    public CommentDiscViewModel PersonalReview { get; set; }
}

public class MovieDiscViewModel
{
    public MovieIdVO MovieId { get; set; }

    /// <summary>
    /// 电影中文名
    /// </summary>
    public MovieTitleVO Title { get; set; }

    /// <summary>
    /// 电影英文名
    /// </summary>
    public MovieTitleEnVO TitleEn { get; set; }

    /// <summary>
    /// 影片的更多中文名
    /// </summary>
    public MovieAkaVO Aka { get; set; }

    public List<CelebrityViewModel> Directors { get; set; } = new List<CelebrityViewModel>();
    public List<CelebrityViewModel> Writers { get; set; } = new List<CelebrityViewModel>();
    public List<CelebrityViewModel> Casts { get; set; } = new List<CelebrityViewModel>();

    /// <summary>
    /// 年代
    /// </summary>
    public YearVO Year { get; set; }

    /// <summary>
    /// 上映日期
    /// </summary>
    public PubdatesVO Pubdates { get; set; }

    /// <summary>
    /// 片长
    /// </summary>
    public DurationsVO Durations { get; set; }

    /// <summary>
    /// 类型（代碼串）
    /// </summary>
    public GenresVO Genres { get; set; }
    /// <summary>
    /// 类型（文子串）
    /// </summary>
    public List<CodeValueVO> GenresValue { get; set; } = new List<CodeValueVO>();

    /// <summary>
    /// 语言（代碼串）
    /// </summary>
    public LanguagesVO Languages { get; set; }
    /// <summary>
    /// 语言（文子串）
    /// </summary>
    public List<CodeValueVO> LanguagesValue { get; set; } = new List<CodeValueVO>();

    /// <summary>
    /// 国家地区（代碼串）
    /// </summary>
    public CountriesVO Countries { get; set; }
    /// <summary>
    /// 国家地区（文子串）
    /// </summary>
    public List<CodeValueVO> CountriesValue { get; set; } = new List<CodeValueVO>();

    /// <summary>
    /// 评分
    /// </summary>
    public RatingVO Rating { get; set; }

    /// <summary>
    /// 评价人数
    /// </summary>
    public RatingCountVO RatingCount { get; set; }

    /// <summary>
    /// 豆瓣ID
    /// </summary>
    public DoubanIDVO DoubanID { get; set; }
    /// <summary>
    /// IMDbId
    /// </summary>
    public IMDbIDVO IMDbID { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public SummaryVO Summary { get; set; }

    /// <summary>
    /// 电影海报
    /// </summary>
    public MovieAvatarVO Avatar { get; set; }

    public static MovieDiscViewModel FromEntity(MovieEntity movie)
    {
        var viewModel = new MovieDiscViewModel();
        viewModel.MovieId = movie.MovieId;
        viewModel.Avatar = movie.Avatar;
        viewModel.Title = movie.Title;
        viewModel.TitleEn = movie.TitleEn;
        viewModel.Year = movie.Year;
        viewModel.DoubanID = movie.DoubanID;
        viewModel.Rating = movie.Rating;
        viewModel.Summary = movie.Summary;
        viewModel.Aka = movie.Aka;
        viewModel.Pubdates = movie.Pubdates;
        viewModel.Durations = movie.Durations;
        viewModel.Genres = movie.Genres;
        viewModel.Countries = movie.Countries;
        viewModel.Languages = movie.Languages;
        viewModel.IMDbID = movie.IMDb;
        viewModel.RatingCount = movie.RatingCount;

        #region Directors

        if (movie.DirectorsId == null)
        {
            if (movie.Directors != null)
            {
                var directors = movie.Directors.ToEnumerable().GetEnumerator();
                while (directors.MoveNext())
                {
                    viewModel.Directors.Add(new CelebrityViewModel() { Name = directors.Current });
                }
            }
        }
        else
        {
            var index = 0;
            var directorIds = movie.DirectorsId.ToEnumerable().GetEnumerator();
            var directors = movie.Directors.ToEnumerable().Cast<CelebrityNameVO>().ToArray();
            while (directorIds.MoveNext())
            {
                viewModel.Directors.Add(new CelebrityViewModel() { Id = directorIds.Current, Name = directors.ElementAt(index) });
                index++;
            }
        }

        #endregion Directors

        #region Writers

        if (movie.WritersId == null)
        {
            if (movie.Writers != null)
            {
                var writers = movie.Writers.ToEnumerable().GetEnumerator();
                while (writers.MoveNext())
                {
                    viewModel.Writers.Add(new CelebrityViewModel() { Name = writers.Current });
                }
            }
        }
        else
        {
            var index = 0;
            var writerIds = movie.WritersId.ToEnumerable().GetEnumerator();
            var writers = movie.Writers.ToEnumerable().Cast<CelebrityNameVO>().ToArray();
            while (writerIds.MoveNext())
            {
                viewModel.Writers.Add(new CelebrityViewModel() { Id = writerIds.Current, Name = writers.ElementAt(index) });
                index++;
            }
        }

        #endregion Writers

        #region Casts

        if (movie.CastsId == null)
        {
            if (movie.Casts != null)
            {
                var casts = movie.Casts.ToEnumerable().GetEnumerator();
                while (casts.MoveNext())
                {
                    viewModel.Casts.Add(new CelebrityViewModel() { Name = casts.Current });
                }
            }
        }
        else
        {
            var index = 0;
            var castsId = movie.CastsId.ToEnumerable().GetEnumerator();
            var casts = movie.Casts.ToEnumerable().Cast<CelebrityNameVO>().ToArray();
            while (castsId.MoveNext())
            {
                viewModel.Casts.Add(new CelebrityViewModel() { Id = castsId.Current, Name = casts.ElementAt(index) });
                index++;
            }
        }

        #endregion Casts

        return viewModel;
    }

    public int CommentCount { get; set; } = 0;

    public bool IsPlan { get; set; } = false;
    public bool IsFinish { get; set; } = false;
    public bool IsFavor { get; set; } = false;

    public int PlanCount { get; set; }
    public int FinishCount { get; set; }
    public int FavorCount { get; set; }

    /// <summary>
    /// 当前用户是否是创建者
    /// </summary>
    public bool IsCreate { get; set; } = false;


}


public class CelebrityViewModel
{
    public CelebrityIdVO Id { get; set; }
    public CelebrityNameVO Name { get; set; }
}