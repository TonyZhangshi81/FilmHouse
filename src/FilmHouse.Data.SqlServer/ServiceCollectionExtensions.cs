using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.SqlServer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Data.SqlServer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqlServerStorage(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(IRepository<>), typeof(SqlServerDbContextRepository<>));

        services.AddDbContext<SqlServerFilmHouseDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString, optionsBuilder =>
                {
                    optionsBuilder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
                })
                .EnableDetailedErrors());

        return services;
    }
}