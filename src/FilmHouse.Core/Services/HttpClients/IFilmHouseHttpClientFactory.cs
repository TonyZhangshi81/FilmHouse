using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Services.HttpClients;

namespace FilmHouse.Core.Services.HttpClients;

/// <summary>
/// 
/// </summary>
[ServiceRegister(FilmHouseServiceLifetime.Transient)]
public interface IFilmHouseHttpClientFactory
{
    IFilmHouseHttpClient CreateClient(string functionId);
}
