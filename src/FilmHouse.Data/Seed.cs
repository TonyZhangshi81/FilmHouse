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
            var sysDate = new CreatedOnVO(System.DateTime.Now);

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

    private static IEnumerable<ConfigurationEntity> GetInitConfigurationSettings(RequestIdVO uuid, CreatedOnVO dateTime) =>
        new List<ConfigurationEntity>
        {
            new() { RequestId = uuid, Key = "WebSiteSettings:Name", Value = "DEMO", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:SubTitle", Value = "DEMO", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:Version", Value = "0.2.0.0", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:WebpagesEnabled", Value = "false", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:ClientValidationEnabled", Value = "true", CreatedOn = dateTime },
            new() { RequestId = uuid, Key = "WebSiteSettings:UnobtrusiveJavaScriptEnabled", Value = "false", CreatedOn = dateTime },
        };

    private static IEnumerable<CodeMastEntity> GetInitCodeMastSettings(RequestIdVO uuid, CreatedOnVO dateTime) =>
        new List<CodeMastEntity>
        {
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("001"), Name = new CodeValueVO("剧情"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("002"), Name = new CodeValueVO("爱情"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("003"), Name = new CodeValueVO("奇幻"), Order = new SortOrderVO(3), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("004"), Name = new CodeValueVO("惊悚"), Order = new SortOrderVO(4), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("005"), Name = new CodeValueVO("喜剧"), Order = new SortOrderVO(5), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("006"), Name = new CodeValueVO("动作"), Order = new SortOrderVO(6), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("007"), Name = new CodeValueVO("科幻"), Order = new SortOrderVO(7), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("008"), Name = new CodeValueVO("冒险"), Order = new SortOrderVO(8), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("009"), Name = new CodeValueVO("悬疑"), Order = new SortOrderVO(9), CreatedOn  = dateTime },

            new() { RequestId = uuid, Group = new CodeGroupVO("Language"), Code = new CodeKeyVO("001"), Name = new CodeValueVO("英语"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("Language"), Code = new CodeKeyVO("002"), Name = new CodeValueVO("法语"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("Language"), Code = new CodeKeyVO("003"), Name = new CodeValueVO("意大利语"), Order = new SortOrderVO(3), CreatedOn  = dateTime },

            new() { RequestId = uuid, Group = new CodeGroupVO("Country"), Code = new CodeKeyVO("001"), Name = new CodeValueVO("美国"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("Country"), Code = new CodeKeyVO("002"), Name = new CodeValueVO("澳大利亚"), Order = new SortOrderVO(2), CreatedOn  = dateTime },

        };

}