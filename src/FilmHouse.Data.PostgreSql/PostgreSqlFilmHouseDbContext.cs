using FilmHouse.Data.PostgreSql.Configurations;
using FilmHouse.Data.PostgreSql.Infrastructure.Data.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FilmHouse.Data.PostgreSql;

public class PostgreSqlFilmHouseDbContext : FilmHouseDbContext
{
    public PostgreSqlFilmHouseDbContext()
    {
    }

    public PostgreSqlFilmHouseDbContext(DbContextOptions options, IServiceProvider serviceProvider)
        : base(options, serviceProvider)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // `替换了默认的`IMethodCallTranslatorProvider`服务实现为自定义的`FilmHouseMethodCallTranslatorProvider`。这可能是为了自定义LINQ查询中方法调用的转换逻辑。
        optionsBuilder.ReplaceService<IMethodCallTranslatorProvider, FilmHouseMethodCallTranslatorProvider>();
        // `替换了默认的`IValueConverterSelector`服务实现为自定义的`FilmHouseValueConverterSelector`。这可能是为了自定义值转换器的选择逻辑。
        optionsBuilder.ReplaceService<IValueConverterSelector, FilmHouseValueConverterSelector>();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        modelBuilder.ApplyConfiguration(new AskConfiguration());
        modelBuilder.ApplyConfiguration(new CelebrityConfiguration());
        modelBuilder.ApplyConfiguration(new CodeMastConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());
        modelBuilder.ApplyConfiguration(new DiscoveryConfiguration());
        modelBuilder.ApplyConfiguration(new MarkConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new NoticeConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceConfiguration());
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        modelBuilder.ApplyConfiguration(new WorkConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}