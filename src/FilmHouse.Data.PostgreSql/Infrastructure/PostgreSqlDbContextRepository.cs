using FilmHouse.Data.Infrastructure;

namespace FilmHouse.Data.PostgreSql.Infrastructure;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class PostgreSqlDbContextRepository<T> : DbContextRepository<T> where T : class
{
    public PostgreSqlDbContextRepository(PostgreSqlFilmHouseDbContext dbContext)
        : base(dbContext)
    {
    }
}
