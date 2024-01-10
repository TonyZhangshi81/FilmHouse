using FilmHouse.Commands.Comment;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Tests;
using FilmHouse.Tests.Utils;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FilmHouse.Commands.Test.Comment;

[TestFixture]
public class DisplayCommandHandlerTest : TransactionalTestBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task DiosplayComment01()
    {
        var movieId = this.DbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First();
        var result = await this.Mediator.Send(new DisplayCommand(movieId));

        Assert.That(result.Movie, Is.Not.Null);
        Assert.That(result.Movie.MovieId, Dz.EqualTo(movieId));
        Assert.That(result.Comments.Count, Is.EqualTo(4));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task DiosplayComment02()
    {
        var movieId = new MovieIdVO(Guid.NewGuid());
        var result = await this.Mediator.Send(new DisplayCommand(movieId));

        Assert.That(result.Movie, Is.Null);
        Assert.That(result.Comments, Is.Null);
    }
}
