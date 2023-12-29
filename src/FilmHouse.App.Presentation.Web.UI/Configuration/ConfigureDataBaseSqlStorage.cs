using FilmHouse.Data.MySql;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data.SqlServer;

namespace FilmHouse.App.Presentation.Web.UI.Configuration;

public static class ConfigureDataBaseSqlStorage
{
    public static IServiceCollection AddDataBaseSqlStorage(this IServiceCollection services, string dbType, string connStr)
    {
        switch (dbType!.ToLower())
        {
            case "mysql":
                services.AddMySqlStorage(connStr!);
                break;
            case "postgresql":
                services.AddPostgreSqlStorage(connStr!);
                break;
            case "sqlserver":
            default:
                services.AddSqlServerStorage(connStr!);
                break;
        }
        return services;
    }
}