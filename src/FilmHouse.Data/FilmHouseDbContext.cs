using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FilmHouse.Data;

public class FilmHouseDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IServiceProvider _serviceProvider;

    public FilmHouseDbContext()
    {
    }

    public FilmHouseDbContext(DbContextOptions options, IServiceProvider serviceProvider)
        : base(options)
    {
        this._serviceProvider = serviceProvider;
        this._loggerFactory = this._serviceProvider.GetRequiredService<ILoggerFactory>();
    }

    /*
     * 1. SQLines tools can help you transfer data, convert database schema (DDL), views, stored procedures, functions, triggers, queries and SQL scripts from Microsoft SQL Server (MSSQL, MS SQL), Azure SQL Database, Azure Synapse to PostgreSQL (Postgres).
     *    (Microsoft SQL Server (MS SQL) to PostgreSQL Migration http://www.sqlines.com/sql-server-to-postgresql)
     * 
     * 2. SQLines provides tools to help you transfer data, convert database schema (DDL), views, stored procedures, functions, triggers, queries and SQL scripts from Microsoft SQL Server (MSSQL, MS SQL), Azure SQL Database, Azure Synapse to MySQL.
     *    (Microsoft SQL Server (MS SQL) to MySQL Migration http://www.sqlines.com/sql-server-to-mysql)
     */

    public virtual DbSet<AlbumEntity> Albums { get; set; }
    public virtual DbSet<AskEntity> Asks { get; set; }
    public virtual DbSet<CelebrityEntity> Celebrities { get; set; }
    public virtual DbSet<CodeMastEntity> CodeMast { get; set; }
    public virtual DbSet<CommentEntity> Comments { get; set; }
    public virtual DbSet<ConfigurationEntity> Configuration { get; set; }
    public virtual DbSet<DiscoveryEntity> Discoveries { get; set; }
    public virtual DbSet<MarkEntity> Marks { get; set; }
    public virtual DbSet<MovieEntity> Movies { get; set; }
    public virtual DbSet<NoticeEntity> Notices { get; set; }
    public virtual DbSet<ResourceEntity> Resources { get; set; }
    public virtual DbSet<UserAccountEntity> UserAccounts { get; set; }
    public virtual DbSet<WorkEntity> Works { get; set; }
    public virtual DbSet<FindPasswordEntity> FindPwds { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 配置使用指定的`loggerFactory`来记录数据库操作日志。
        // 配置是否启用敏感数据记录。`false`表示禁用敏感数据记录。
        optionsBuilder
            .UseLoggerFactory(this._loggerFactory)
            .EnableSensitiveDataLogging(false);

        // 以下处理暂不实现!
        // `替换了默认的`IMethodCallTranslatorProvider`服务实现为自定义的`MyMethodCallTranslatorProvider`。这可能是为了自定义LINQ查询中方法调用的转换逻辑。
        //optionsBuilder.ReplaceService<IMethodCallTranslatorProvider, MyMethodCallTranslatorProvider>();
        // `替换了默认的`IQuerySqlGeneratorFactory`服务实现为自定义的`MyQuerySqlGeneratorFactory`。这可能是为了自定义SQL查询生成器的逻辑。
        //optionsBuilder.ReplaceService<IQuerySqlGeneratorFactory, MyQuerySqlGeneratorFactory>();
        // `替换了默认的`IValueConverterSelector`服务实现为自定义的`MyValueConverterSelector`。这可能是为了自定义值转换器的选择逻辑。
        //optionsBuilder.ReplaceService<IValueConverterSelector, MyValueConverterSelector>();
        // `替换了默认的`IMemberTranslatorProvider`服务实现为自定义的`MyMemberTranslatorProvider`。这可能是为了自定义成员访问器（属性、字段等）的转换逻辑。
        //optionsBuilder.ReplaceService<IMemberTranslatorProvider, MyMemberTranslatorProvider>();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}

public static class FilmHouseDbContextExtension
{
    public static async Task ClearAllData(this FilmHouseDbContext context)
    {
        context.Albums.RemoveRange();
        context.Asks.RemoveRange();
        context.Celebrities.RemoveRange();
        context.CodeMast.RemoveRange();
        context.Comments.RemoveRange();
        context.Configuration.RemoveRange();
        context.Discoveries.RemoveRange();
        context.Marks.RemoveRange();
        context.Movies.RemoveRange();
        context.Notices.RemoveRange();
        context.Resources.RemoveRange();
        context.UserAccounts.RemoveRange();
        context.Works.RemoveRange();
        context.FindPwds.RemoveRange();

        await context.SaveChangesAsync();
    }
}