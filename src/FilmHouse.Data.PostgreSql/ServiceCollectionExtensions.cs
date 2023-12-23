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

        // 用于禁用 PostgreSQL 数据库中特殊的日期时间值（无限大/无限小）与 .NET 中的对应转换
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        // 用于启用或禁用旧版的时间戳行为
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<PostgreSqlFilmHouseDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseNpgsql(connectionString, optionsBuilder =>
                {
                    optionsBuilder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
                })
                .EnableDetailedErrors());

        return services;
    }
}