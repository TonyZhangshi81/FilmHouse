using FilmHouse.Commands.Album;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Tests;
using FilmHouse.Tests.Utils;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FilmHouse.Commands.Test.Album;

[TestFixture]
public class DisplayCommandHandlerTest : TransactionalTestBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task DisplayCommand01()
    {
        var result = await this.Mediator.Send(new DisplayCommand());

        Assert.That(result.Albums.Count, Is.EqualTo(6));

        var albumId = result.Albums.Where(d => d.Title == new AlbumTitleVO("影集A")).Select(d => d.AlbumId).First();
        Assert.That(result.AlbumFollows.Where(d => d.AlbumId == albumId).Select(d => d.FollowCount).FirstOrDefault(), Dz.EqualTo(6));
    }

}
