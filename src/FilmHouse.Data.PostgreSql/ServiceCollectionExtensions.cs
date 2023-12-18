using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.PostgreSql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Data.PostgreSql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgreSqlStorage(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(IRepository<>), typeof(PostgreSqlDbContextRepository<>));

        // 在这里切换EFCore中是否禁用postgresql infinity的选项设置
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<PostgreSqlFilmHouseDbContext>(options => options
            .UseLazyLoadingProxies()
            .EnableDetailedErrors()
            .UseNpgsql(connectionString, options =>
            {
                options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
            }));

        return services;
    }
}