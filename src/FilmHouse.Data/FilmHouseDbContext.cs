using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data;

public class FilmHouseDbContext : DbContext
{
    public FilmHouseDbContext()
    {
    }

    public FilmHouseDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<AlbumEntity> Album { get; set; }
    public virtual DbSet<ConfigurationEntity> Configuration { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());
    }
}

public static class FilmHouseDbContextExtension
{
    public static async Task ClearAllData(this FilmHouseDbContext context)
    {
        context.Album.RemoveRange();
        context.Configuration.RemoveRange();

        await context.SaveChangesAsync();
    }
}