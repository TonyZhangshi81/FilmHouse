using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.PostgreSql;

public class PostgreSqlFilmHouseDbContext : FilmHouseDbContext
{
    public PostgreSqlFilmHouseDbContext()
    {
    }

    public PostgreSqlFilmHouseDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }
}