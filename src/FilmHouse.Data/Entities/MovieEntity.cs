using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class MovieEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public MovieIdVO MovieId { get; set; }

    [Column(Order = 3)]
    [Required]
    public MovieTitleVO Title { get; set; }

    [Column(Order = 4)]
    public MovieTitleEnVO TitleEn { get; set; }

    [Column(Order = 5)]
    public MovieAkaVO Aka { get; set; }

    [Column(Order = 6)]
    public DirectorNamesVO Directors { get; set; }

    [Column(Order = 7)]
    public WritersNamesVO Writers { get; set; }

    [Column(Order = 8)]
    public CastsNamesVO Casts { get; set; }

    [Column(Order = 9)]
    public DirectorsIdVO DirectorsId { get; set; }

    [Column(Order = 10)]
    public WritersIdVO WritersId { get; set; }

    [Column(Order = 11)]
    public CastsIdVO CastsId { get; set; }

    [Column(Order = 12)]
    public YearVO Year { get; set; }

    [Column(Order = 13)]
    public PubdatesVO Pubdates { get; set; }

    [Column(Order = 14)]
    public DurationsVO Durations { get; set; }

    [Column(Order = 15)]
    public GenresVO Genres { get; set; }

    [Column(Order = 16)]
    public LanguagesVO Languages { get; set; }

    [Column(Order = 17)]
    public CountriesVO Countries { get; set; }

    [Column(Order = 18)]
    public RatingVO Rating { get; set; }

    [Column(Order = 19)]
    public RatingCountVO RatingCount { get; set; }

    [Column(Order = 20)]
    public DoubanIDVO DoubanID { get; set; }

    [Column(Order = 21)]
    public IMDbVO IMDb { get; set; }

    [Column(Order = 22)]
    public SummaryVO Summary { get; set; }

    [Column(Order = 23)]
    public MovieAvatarVO Avatar { get; set; }

    [Column(Order = 24)]
    [Required]
    public UserIdVO UserId { get; set; }

    [Column(Order = 25)]
    public ReviewStatusVO ReviewStatus { get; set; }

    [Column(Order = 26)]
    public NoteVO Note { get; set; }

    [Column(Order = 27)]
    public PageViewsVO PageViews { get; set; }
    
}
