using FilmHouse.Core.Utils;
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


#if DEBUG

            await dbContext.UserAccounts.AddRangeAsync(GetUserAccounts(uuid, sysDate));
            await dbContext.SaveChangesAsync();

            await dbContext.Movies.AddRangeAsync(GetMovies(uuid, sysDate, dbContext.UserAccounts.First().UserId));
            await dbContext.SaveChangesAsync();

            await dbContext.Discoveries.AddRangeAsync(GetDiscoveries(uuid, sysDate, dbContext.Movies.ToList()));
            await dbContext.SaveChangesAsync();
#endif

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

    /// <summary>
    /// 配置信息
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static IEnumerable<ConfigurationEntity> GetInitConfigurationSettings(RequestIdVO uuid, CreatedOnVO dateTime) =>
        new List<ConfigurationEntity>
        {
            new() { RequestId = uuid, Key = new("WebSiteSettings:Name"), Value = new("DEMO"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:SubTitle"), Value = new("DEMO"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:Version"), Value = new("0.2.0.0"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:WebpagesEnabled"), Value = new("false"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:ClientValidationEnabled"), Value = new("true"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:UnobtrusiveJavaScriptEnabled"), Value = new("false"), CreatedOn = dateTime },
        };

    /// <summary>
    /// 代码信息管理表
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static IEnumerable<UserAccountEntity> GetUserAccounts(RequestIdVO uuid, CreatedOnVO dateTime) =>
       new List<UserAccountEntity>
       {
            new(){ RequestId = uuid, UserId = new UserIdVO(Guid.NewGuid()), Account = new AccountNameVO("tonyzhangshi"), Password = new PasswordVO(new PasswordVO("123456").ToHash("test")), EmailAddress = new EmailAddressVO("tonyzhangshi@163.com"), Avatar = new UserAvatarVO("0ACFC82E7D5A41FC8AB8FD4EF603C858Tony.jpg"), Cover = new CoverVO("Cover_1.jpg"), IsAdmin = new IsAdminVO(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new UserIdVO(Guid.NewGuid()), Account = new AccountNameVO("test01"), Password = new PasswordVO(new PasswordVO("111111").ToHash("test01")), EmailAddress = new EmailAddressVO("test01@163.com"), Avatar = new UserAvatarVO("User_1.jpg"), Cover = new CoverVO("Cover_1.jpg"), IsAdmin = new IsAdminVO(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new UserIdVO(Guid.NewGuid()), Account = new AccountNameVO("test02"), Password = new PasswordVO(new PasswordVO("222222").ToHash("test02")), EmailAddress = new EmailAddressVO("test02@163.com"), Avatar = new UserAvatarVO("User_1.jpg"), Cover = new CoverVO("Cover_1.jpg"), IsAdmin = new IsAdminVO(true), CreatedOn = dateTime },
       };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static IEnumerable<MovieEntity> GetMovies(RequestIdVO uuid, CreatedOnVO dateTime, UserIdVO userId)
    {
        var list = new List<MovieEntity>()
        {
            new MovieEntity() { RequestId = uuid, MovieId = new MovieIdVO(Guid.NewGuid()), Title = new MovieTitleVO("Title001"), UserId = userId, CreatedOn = dateTime },
            new MovieEntity() { RequestId = uuid, MovieId = new MovieIdVO(Guid.NewGuid()), Title = new MovieTitleVO("Title002"), UserId = userId, CreatedOn = dateTime },
            new MovieEntity() { RequestId = uuid, MovieId = new MovieIdVO(Guid.NewGuid()), Title = new MovieTitleVO("Title003"), UserId = userId, CreatedOn = dateTime },
        };

        return list;
    }


    private static IEnumerable<DiscoveryEntity> GetDiscoveries(RequestIdVO uuid, CreatedOnVO dateTime, List<MovieEntity> movies) =>
       new List<DiscoveryEntity>
       {
                new DiscoveryEntity(){ RequestId = uuid, DiscoveryId = new DiscoveryIdVO(Guid.NewGuid()), MovieId = movies.ElementAt(0).MovieId, Avatar = new DiscoveryAvatarVO("p2305422832.jpg"), Order = new SortOrderVO(1), CreatedOn = dateTime },
                new DiscoveryEntity(){ RequestId = uuid, DiscoveryId = new DiscoveryIdVO(Guid.NewGuid()), MovieId = movies.ElementAt(1).MovieId, Avatar = new DiscoveryAvatarVO("p2220223845.jpg"), Order = new SortOrderVO(2), CreatedOn = dateTime },
                new DiscoveryEntity(){ RequestId = uuid, DiscoveryId = new DiscoveryIdVO(Guid.NewGuid()), MovieId = movies.ElementAt(2).MovieId, Avatar = new DiscoveryAvatarVO("20231022164431.jpg"), Order = new SortOrderVO(3), CreatedOn = dateTime },
       };

}