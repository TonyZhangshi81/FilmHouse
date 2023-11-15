using FilmHouse.Data.PostgreSql.Configurations;
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
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        modelBuilder.ApplyConfiguration(new AskConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new CodeMastConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}