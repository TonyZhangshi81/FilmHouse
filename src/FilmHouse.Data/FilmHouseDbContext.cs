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
    public virtual DbSet<UserAccountEntity> UserAccounts { get; set; }

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
        context.UserAccounts.RemoveRange();

        await context.SaveChangesAsync();
    }
}