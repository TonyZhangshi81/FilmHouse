
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Mvc.Health;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomerHealthChecks(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connStr = configuration.GetConnectionString("FilmHouseDatabase");

        services
            .AddHealthChecks()
            .AddNpgSql(connStr!)
            .AddCheck<LogfileHealthCheck>("Log files");

        return services;
    }
}