
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FilmHouse.Core.Presentation.Web.Health;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomerHealthChecks(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

        var connectionString = configuration.GetConnectionString("FilmHouseDatabase");
        var dbType = configuration.GetConnectionString("DatabaseType");

        var hcBuilder = services.AddHealthChecks();

        switch (dbType!.ToLower())
        {
            case "mysql":
                hcBuilder.AddMySql(connectionString, name: "mysql db-connection-check", tags: new string[] { "db", "sql", "mysql", "ready" });
                break;
            case "postgresql":
                hcBuilder.AddNpgSql(connectionString, name: "postgresql db-connection-check", tags: new string[] { "db", "sql", "postgresql", "ready" });
                break;
            case "sqlserver":
            default:
                hcBuilder.AddSqlServer(connectionString, name: "sqlserver db-connection-check", tags: new string[] { "db", "sql", "sqlserver", "ready" });
                break;
        }

        hcBuilder.AddCheck<DiskSpaceHealthCheck>("diskspace health check", tags: new string[] { "system", "diskspace", "ready" });

        hcBuilder.AddCheck<MemoryHealthCheck>("memory health check", tags: new string[] { "system", "memory", "ready" });

        return services;
    }
}