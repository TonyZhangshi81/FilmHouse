using FilmHouse.Core.Services.MongoBasicOperation;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FilmHouse.Core.Presentation.Web.Health;

public class MongoHealthCheck : IHealthCheck
{
    private IMongoClient _client;
    private IMongoDatabase _database;

    public MongoHealthCheck(IOptions<MongoDBContextOptions> options)
    {
        this._client = new MongoClient(options.Value.Connection);

        this._database = this._client.GetDatabase(options.Value.DatabaseName);

    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {

        var healthCheckResultHealthy = await CheckMongoDBConnectionAsync();

        if (healthCheckResultHealthy)
        {
            return HealthCheckResult.Healthy("MongoDB health check success");
        }

        return HealthCheckResult.Unhealthy("MongoDB health check failure");
    }

    private async Task<bool> CheckMongoDBConnectionAsync()
    {
        try
        {
            await this._database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
        }

        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
