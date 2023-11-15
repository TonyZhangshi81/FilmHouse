using Microsoft.Extensions.Logging;
using FilmHouse.Data.Entities;
using FilmHouse.Data;

namespace FilmHouse.Data;

public class Seed
{
    public static async Task SeedAsync(FilmHouseDbContext dbContext, ILogger logger, int maxRetryAvailability, int retry = 0)
    {
        var retryForAvailability = retry;

        try
        {
            await dbContext.Configuration.AddRangeAsync(GetInitConfigurationSettings(Guid.NewGuid(), System.DateTime.Now));

            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (retryForAvailability >= maxRetryAvailability)
            {
                throw;
            }

            retryForAvailability++;

            logger.LogError(e.Message);
            await SeedAsync(dbContext, logger, maxRetryAvailability, retryForAvailability);
            throw;
        }
    }

    private static IEnumerable<ConfigurationEntity> GetInitConfigurationSettings(Guid uuid, DateTime dateTime) =>
        new List<ConfigurationEntity>
        {
            new() { RequestId = uuid, Key = "WebSiteSettings:Name", Value = "DEMO", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:SubTitle", Value = "DEMO", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:Version", Value = "0.2.0.0", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:WebpagesEnabled", Value = "false", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:ClientValidationEnabled", Value = "true", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:UnobtrusiveJavaScriptEnabled", Value = "false", CreatedOn = dateTime },
        };

}