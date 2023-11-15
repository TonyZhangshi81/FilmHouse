using FilmHouse.Data.MySql.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.MySql;

public class MySqlFilmHouseDbContext : FilmHouseDbContext
{
    public MySqlFilmHouseDbContext()
    {
    }

    public MySqlFilmHouseDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        modelBuilder.ApplyConfiguration(new AskConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new CodeMastConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}