using Microsoft.Extensions.Logging;
using FilmHouse.Data.Entities;
using FilmHouse.Data;

namespace FilmHouse.Data;

public class Seed
{
    public static async Task SeedAsync(FilmHouseDbContext dbContext, ILogger logger, int retry = 0)
    {
        var retryForAvailability = retry;

        try
        {
            await dbContext.Album.AddRangeAsync(GetLocalAccounts());

            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(e.Message);
            await SeedAsync(dbContext, logger, retryForAvailability);
            throw;
        }
    }

    private static IEnumerable<AlbumEntity> GetLocalAccounts() =>
        new List<AlbumEntity>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "001",
                User = "test01",
                Item = "",
                Summary = "",
                Time = DateTime.UtcNow,
                AlterTime = DateTime.UtcNow,
                Cover = "",
                Visit = 0
            }
        };

}