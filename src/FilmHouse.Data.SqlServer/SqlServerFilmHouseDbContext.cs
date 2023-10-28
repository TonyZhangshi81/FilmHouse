using FilmHouse.Data.SqlServer.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.SqlServer;

public class SqlServerFilmHouseDbContext : FilmHouseDbContext
{
    public SqlServerFilmHouseDbContext()
    {
    }

    public SqlServerFilmHouseDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}