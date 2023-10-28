using FilmHouse.Data.Infrastructure;

namespace FilmHouse.Data.SqlServer.Infrastructure;


public class SqlServerDbContextRepository<T> : DbContextRepository<T> where T : class
{
    public SqlServerDbContextRepository(SqlServerFilmHouseDbContext dbContext)
        : base(dbContext)
    {
    }
}