using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Services.HttpClients;

namespace FilmHouse.App.Presentation.Web.UI.HttpClients.Nginx;

public partial class HttpClientConfiguration : HttpClientConfigurationBase
{
    public HttpClientConfiguration(IConfiguration configuration)
    : base(configuration)
    {
    }

    protected override string GetBaseAddress() => "/api/Nginx";
}
