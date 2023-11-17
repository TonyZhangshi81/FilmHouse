using FilmHouse.Data.PostgreSql.Configurations;
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
        modelBuilder.ApplyConfiguration(new AskConfiguration());
        modelBuilder.ApplyConfiguration(new CelebrityConfiguration());
        modelBuilder.ApplyConfiguration(new CodeMastConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}