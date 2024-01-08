using FilmHouse.Commands.Movie;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Tests;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FilmHouse.Commands.Test.Movie;

[TestFixture]
public class IsExistWithMovieIdCommandHandlerTest : TransactionalTestBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task IsExistWithMovieId01()
    {
        var movieId = this.DbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First();
        var result = await this.Mediator.Send(new IsExistWithMovieIdCommand(movieId));

        Assert.That(result, Is.True);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task IsExistWithMovieId02()
    {
        var result = await this.Mediator.Send(new IsExistWithMovieIdCommand(new MovieIdVO(Guid.NewGuid())));

        Assert.That(result, Is.False);
    }
}
