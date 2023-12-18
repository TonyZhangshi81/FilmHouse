using FilmHouse.Commands.Test.Utils;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.MySql;
using FilmHouse.Data.MySql.Infrastructure;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data.PostgreSql.Infrastructure;
using FilmHouse.Data.SqlServer;
using FilmHouse.Data.SqlServer.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Commands.Test
{
    public class TestStartup
    {
        public TestStartup(IConfiguration config)
        {
        }

        public void Configure(IApplicationBuilder builder)
        {
            builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // 缓存管理
            services.AddMemoryCache();

            // DI的批量登录设定
            var assemblies = StartupCore.GetAppAssemblies(true);
            services.AddLocalService(assemblies);


            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            services.AddSingleton<IConfiguration>(configuration);


            var dbType = configuration.GetValue<string>("ConnectionStrings:DatabaseType");
            var connectionString = configuration.GetValue<string>("ConnectionStrings:FilmHouseDatabase");


            switch (dbType.ToLower())
            {
                case "mysql":
                    services.AddScoped(typeof(IRepository<>), typeof(MySqlDbContextRepository<>));
                    services.AddDbContext<MySqlFilmHouseDbContext>(optionsAction => optionsAction.UseLazyLoadingProxies()
                        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder =>
                        {
                            builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
                        })
                        .EnableDetailedErrors());
                    break;
                case "postgresql":
                    services.AddScoped(typeof(IRepository<>), typeof(PostgreSqlDbContextRepository<>));
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    services.AddDbContext<PostgreSqlFilmHouseDbContext>(options => options
                        .UseLazyLoadingProxies()
                        .EnableDetailedErrors()
                        .UseNpgsql(connectionString, options =>
                        {
                            options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
                        }));
                    break;
                case "sqlserver":
                default:
                    services.AddScoped(typeof(IRepository<>), typeof(SqlServerDbContextRepository<>));
                    services.AddDbContext<SqlServerFilmHouseDbContext>(options =>
                        options.UseLazyLoadingProxies()
                            .UseSqlServer(connectionString, builder =>
                            {
                                builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
                            }).
                            EnableDetailedErrors());
                    break;
            }
        }
    }
}
