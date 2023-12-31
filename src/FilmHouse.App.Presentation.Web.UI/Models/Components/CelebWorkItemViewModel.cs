using System.Text;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.App.Presentation.Web.UI.Models.Components
{
    public class CelebWorkItemViewModel
    {
        /// <summary>
        /// 电影ID
        /// </summary>
        public MovieIdVO MovieId { get; set; }
        /// <summary>
        /// 电影中文名
        /// </summary>
        public MovieTitleVO Title { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public RatingVO Rating { get; set; }
        /// <summary>
        /// 年代
        /// </summary>
        public YearVO Year { get; set; }
        /// <summary>
        /// 电影海报
        /// </summary>
        public MovieAvatarVO Avatar { get; set; }
        /// <summary>
        /// 导演名集合
        /// </summary>
        public List<CelebrityNameVO> Directors { get; set; } = new List<CelebrityNameVO>();
        /// <summary>
        /// 主演名集合
        /// </summary>
        public List<CelebrityNameVO> Casts { get; set; } = new List<CelebrityNameVO>();

        /// <summary>
        /// 类型（代碼串）
        /// </summary>
        public GenresVO Genres { get; set; }
        /// <summary>
        /// 类型（文字串）
        /// </summary>
        public List<CodeValueVO> GenresValue { get; set; } = new List<CodeValueVO>();

        /// <summary>
        /// 当前影片中该明星所担当的职责
        /// </summary>
        public InMovieProfessionsVO InMovieProfessions { get; set; }

        public static CelebWorkItemViewModel FromEntity(MovieEntity movie, CelebrityIdVO celebrityId)
        {
            var viewModel = new CelebWorkItemViewModel();
            viewModel.MovieId = movie.MovieId;
            viewModel.Title = movie.Title;
            viewModel.Rating = movie.Rating;
            viewModel.Year = movie.Year;
            viewModel.Genres = movie.Genres;
            viewModel.Avatar = movie.Avatar;

            var temp = new StringBuilder();
            temp.Append("[");
            if(movie.DirectorsId != null)
            {
                temp.Append(movie.DirectorsId.AsPrimitive().Contains(celebrityId.AsPrimitive().ToString()) ? " 导演 " : string.Empty);
            }
            if(movie.WritersId != null)
            {
                temp.Append(movie.WritersId.AsPrimitive().Contains(celebrityId.AsPrimitive().ToString()) ? " 编剧 " : string.Empty);
            }
            if (movie.CastsId != null)
            {
                temp.Append(movie.CastsId.AsPrimitive().Contains(celebrityId.AsPrimitive().ToString()) ? " 演员 " : string.Empty);
            }
            temp.Append("]");
            viewModel.InMovieProfessions = new InMovieProfessionsVO(temp.Length > 2 ? temp.ToString() : string.Empty);

            #region Directors

            if (movie.DirectorsId == null)
            {
                if (movie.Directors != null)
                {
                    var directors = movie.Directors.ToEnumerable().GetEnumerator();
                    while (directors.MoveNext())
                    {
                        viewModel.Directors.Add(new(directors.Current.AsPrimitive()));
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
                    viewModel.Directors.Add(new(directors.ElementAt(index).AsPrimitive()));
                    index++;
                }
            }

            #endregion Directors

            #region Casts

            if (movie.CastsId == null)
            {
                if (movie.Casts != null)
                {
                    var casts = movie.Casts.ToEnumerable().GetEnumerator();
                    while (casts.MoveNext())
                    {
                        viewModel.Casts.Add(new(casts.Current.AsPrimitive()));
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
                    viewModel.Casts.Add(new(casts.ElementAt(index).AsPrimitive()));
                    index++;
                }
            }

            #endregion Casts

            return viewModel;
        }

    }
}
