namespace FilmHouse.Business;

public class WebSiteSettings
{
    public string Name { get; set; } = "DEMO";

    public string SubTitle { get; set; } = "DEMO";

    public string Version { get; set; } = "0.2.0.0";

    public string WebpagesEnabled { get; set; } = "false";

    public bool ClientValidationEnabled { get; set; } = true;

    public bool UnobtrusiveJavaScriptEnabled { get; set; } = true;
}