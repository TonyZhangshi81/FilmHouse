using Microsoft.EntityFrameworkCore;
using FilmHouse.Data.MySql;
using FilmHouse.Data.SqlServer;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data;
using FilmHouse.Data.Core.Services.Codes;
using FilmHouse.Data.Infrastructure.Services.Codes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;

namespace FilmHouse.Web;

public static class WebApplicationExtensions
{
    public static async Task<StartupInitResult> InitStartUp(this WebApplication app, string dbType)
    {
        if (string.IsNullOrEmpty(dbType))
        {
            throw new ArgumentOutOfRangeException(nameof(dbType));
        }

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        //var env = services.GetRequiredService<IWebHostEnvironment>();
        var maxRetryAvailability = Convert.ToInt32(app.Configuration.GetSection("SeedMaxRetryAvailability").Value ?? "10");

        FilmHouseDbContext context = dbType.ToLowerInvariant() switch
        {
            "mysql" => services.GetRequiredService<MySqlFilmHouseDbContext>(),
            "sqlserver" => services.GetRequiredService<SqlServerFilmHouseDbContext>(),
            "postgresql" => services.GetRequiredService<PostgreSqlFilmHouseDbContext>(),
            _ => throw new ArgumentOutOfRangeException(nameof(dbType))
        };

        try
        {
            await context.Database.EnsureCreatedAsync();
        }
        catch (Exception e)
        {
            app.Logger.LogCritical(e, e.Message);
            return StartupInitResult.DatabaseConnectionFail;
        }

        bool isNew = !await context.Configuration.AnyAsync();
        if (isNew)
        {
            try
            {
                app.Logger.LogInformation("建立数据库...");

                await context.ClearAllData();
                await Seed.SeedAsync(context, app.Logger, maxRetryAvailability);

                app.Logger.LogInformation("数据库建立成功");
            }
            catch (Exception e)
            {
                app.Logger.LogCritical(e, e.Message);
                return StartupInitResult.DatabaseSetupFail;
            }
        }

        try
        {
            app.Logger.LogInformation("通用代码信息数据获取...");

            // 代码管理的缓存化
            using (var innerScope = services.CreateScope())
            {
                // IMemoryCache要在应用程序侧的主机上使用，所以要从host生成的scope中获取
                var memoryCache = innerScope.ServiceProvider.GetRequiredService<IMemoryCache>();
                var codeCaher = (ICodeProviderCacher)new CodeProvider(context, memoryCache);
                codeCaher.EnsureCache();
            }

            app.Logger.LogInformation("通用代码信息数据已获取并设定缓存");
        }
        catch (Exception e)
        {
            app.Logger.LogCritical(e, e.Message);
            return StartupInitResult.CodeDataCacheFail;
        }

        return StartupInitResult.None;
    }
}

public enum StartupInitResult
{
    None = 0,
    DatabaseConnectionFail = 1,
    DatabaseSetupFail = 2,
    CodeDataCacheFail = 3
}