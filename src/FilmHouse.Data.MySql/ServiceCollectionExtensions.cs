using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Data.MySql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMySqlStorage(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(IRepository<>), typeof(MySqlDbContextRepository<>));

        services.AddDbContext<MySqlFilmHouseDbContext>(options => options
                    .UseLazyLoadingProxies()
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), optionsBuilder =>
                    {
                        optionsBuilder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
                    })
                    .EnableDetailedErrors());

        return services;
    }
}