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

    // dotnet add package ErikEJ.EntityFrameworkCore.SqlServer.DateOnlyTimeOnly
    // Adds .NET 6 or later DateOnly and TimeOnly support to the SQL Server EF Core provider. These types map directly to the SQL Server date and time data types.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(x => x.UseDateOnlyTimeOnly());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new AskConfiguration());
        modelBuilder.ApplyConfiguration(new CelebrityConfiguration());
        modelBuilder.ApplyConfiguration(new CodeMastConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());
        modelBuilder.ApplyConfiguration(new MarkConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new NoticeConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceConfiguration());
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        modelBuilder.ApplyConfiguration(new WorkConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}