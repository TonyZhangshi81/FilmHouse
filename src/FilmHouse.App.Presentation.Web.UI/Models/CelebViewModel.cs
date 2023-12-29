using FilmHouse.App.Presentation.Web.UI.Models;

namespace FilmHouse.App.Presentation.Web.UI.Models;

public class CelebViewModel
{
    public CelebDiscViewModel Album { get; set; } = new CelebDiscViewModel();
}

public class CelebDiscViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string NameEn { get; set; }

    public string Aka { get; set; }

    public string AkaEn { get; set; }

    public string Gender { get; set; }

    public string Birthday { get; set; }

    public string Deathday { get; set; }

    public string BornPlace { get; set; }

    public string Pro { get; set; }

    public string Family { get; set; }

    public string DoubanID { get; set; }

    public string IMDbID { get; set; }

    public string Summary { get; set; }

    public string SummaryShort { get; set; }

    public string Avatar { get; set; }
}
