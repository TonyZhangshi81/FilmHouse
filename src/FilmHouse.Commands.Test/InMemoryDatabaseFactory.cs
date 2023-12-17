using FilmHouse.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Commands.Test;

public class InMemoryDatabaseFactory
{
    private readonly string databaseName;

    public InMemoryDatabaseFactory()
    {
        this.databaseName = Guid.NewGuid().ToString();
    }

    public InMemoryDatabaseFactory(string databaseName)
    {
        this.databaseName = databaseName;
    }

    public FilmHouseDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<FilmHouseDbContext>()
                            .UseInMemoryDatabase(databaseName: this.databaseName)
                            .Options;

        return new FilmHouseDbContext(options);
    }
}