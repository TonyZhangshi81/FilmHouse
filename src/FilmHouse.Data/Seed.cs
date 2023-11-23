using FilmHouse.Data.Core.ValueObjects;
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
            var uuid = new RequestIdVO(Guid.NewGuid());
            var sysDate = new SysDateTimeVO(System.DateTime.Now);

            await dbContext.Configuration.AddRangeAsync(GetInitConfigurationSettings(uuid, sysDate));
            await dbContext.CodeMast.AddRangeAsync(GetInitCodeMastSettings(uuid, sysDate));

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

    private static IEnumerable<ConfigurationEntity> GetInitConfigurationSettings(RequestIdVO uuid, SysDateTimeVO dateTime) =>
        new List<ConfigurationEntity>
        {
            new() { RequestId = uuid, Key = "WebSiteSettings:Name", Value = "DEMO", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:SubTitle", Value = "DEMO", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:Version", Value = "0.2.0.0", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:WebpagesEnabled", Value = "false", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:ClientValidationEnabled", Value = "true", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:UnobtrusiveJavaScriptEnabled", Value = "false", CreatedOn = dateTime },
        };

    private static IEnumerable<CodeMastEntity> GetInitCodeMastSettings(RequestIdVO uuid, SysDateTimeVO dateTime) =>
        new List<CodeMastEntity>
        {
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "001", CodeValue = "剧情", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "002", CodeValue = "爱情", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "003", CodeValue = "奇幻", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "004", CodeValue = "惊悚", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "005", CodeValue = "喜剧", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "006", CodeValue = "动作", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "007", CodeValue = "科幻", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "008", CodeValue = "冒险", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "GenreMovie", CodeId = "009", CodeValue = "悬疑", CreatedOn  = dateTime },

            new() { RequestId = uuid, Type = "Language", CodeId = "001", CodeValue = "英语", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "Language", CodeId = "002", CodeValue = "法语", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "Language", CodeId = "003", CodeValue = "意大利语", CreatedOn  = dateTime },

            new() { RequestId = uuid, Type = "Country", CodeId = "001", CodeValue = "美国", CreatedOn  = dateTime },
            new() { RequestId = uuid, Type = "Country", CodeId = "002", CodeValue = "澳大利亚", CreatedOn  = dateTime },

        };

}