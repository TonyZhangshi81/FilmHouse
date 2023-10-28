using Microsoft.EntityFrameworkCore;
using FilmHouse.Data.MySql;
using FilmHouse.Data.SqlServer;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data;

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
        var env = services.GetRequiredService<IWebHostEnvironment>();

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
                app.Logger.LogInformation("Seeding database...");

                await context.ClearAllData();
                await Seed.SeedAsync(context, app.Logger);

                app.Logger.LogInformation("Database seeding successfully.");

            }
            catch (Exception e)
            {
                app.Logger.LogCritical(e, e.Message);
                return StartupInitResult.DatabaseSetupFail;
            }
        }

        return StartupInitResult.None;
    }
}

public enum StartupInitResult
{
    None = 0,
    DatabaseConnectionFail = 1,
    DatabaseSetupFail = 2
}