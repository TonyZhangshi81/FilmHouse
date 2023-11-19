
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FilmHouse.Mvc.Health;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomerHealthChecks(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connStr = configuration.GetConnectionString("FilmHouseDatabase");
        var dbType = configuration.GetConnectionString("DatabaseType");

        var hcBuilder = services.AddHealthChecks();

        switch (dbType!.ToLower())
        {
            case "mysql":
                hcBuilder.AddMySql(connStr, name: "mysql db-connection-check", tags: new string[] { "FilmHouse" });
                break;
            case "postgresql":
                hcBuilder.AddNpgSql(connStr, name: "postgresql db-connection-check", tags: new string[] { "FilmHouse" });
                break;
            case "sqlserver":
            default:
                hcBuilder.AddSqlServer(connStr, name: "sqlserver db-connection-check", tags: new string[] { "FilmHouse" });
                break;
        }

        // customer health check
        hcBuilder.AddCheck<DiskSpaceHealthCheck>("DiskSpace Health Check");
        hcBuilder.AddCheck<MemoryHealthCheck>("Memory Health Check");
        hcBuilder.AddCheck<LogfileHealthCheck>("Log files");

        return services;
    }
}