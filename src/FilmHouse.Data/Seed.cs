using FilmHouse.Data.Entities;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Data;

public class Seed
{
    public static async Task SeedAsync(FilmHouseDbContext dbContext, ILogger logger, int maxRetryAvailability, int retry = 0)
    {
        var retryForAvailability = retry;

        try
        {
            var uuid = Guid.NewGuid();
            var sysDate = System.DateTime.Now;

            await dbContext.Configuration.AddRangeAsync(GetInitConfigurationSettings(uuid, sysDate));
            await dbContext.CodeMasts.AddRangeAsync(GetInitCodeMastSettings(uuid, sysDate));

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

    private static IEnumerable<CodeMastEntity> GetInitCodeMastSettings(Guid uuid, DateTime dateTime) =>
        new List<CodeMastEntity>
        {
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "gm001", CodeValue = "剧情", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "gm002", CodeValue = "爱情", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "gm003", CodeValue = "奇幻", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "gm004", CodeValue = "惊悚", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "gm005", CodeValue = "喜剧", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "gm006", CodeValue = "动作", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "gm007", CodeValue = "科幻", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "gm008", CodeValue = "冒险", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "gm009", CodeValue = "悬疑", CreatedOn  = dateTime },

            new() { RequestId = uuid, Type = "Language", CodeId = "l0001", CodeValue = "英语", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "Language", CodeId = "l0002", CodeValue = "法语", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "Language", CodeId = "l0003", CodeValue = "意大利语", CreatedOn  = dateTime },

            new() { RequestId = uuid, Type = "Country", CodeId = "c0001", CodeValue = "美国", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "Country", CodeId = "c0002", CodeValue = "澳大利亚", CreatedOn  = dateTime },
        };

}