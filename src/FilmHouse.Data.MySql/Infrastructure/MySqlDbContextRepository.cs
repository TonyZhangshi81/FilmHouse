using FilmHouse.Data.Infrastructure;

namespace FilmHouse.Data.MySql.Infrastructure;


public class MySqlDbContextRepository<T> : DbContextRepository<T> where T : class
{
    public MySqlDbContextRepository(MySqlFilmHouseDbContext dbContext)
        : base(dbContext)
    {
    }
}