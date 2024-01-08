using FilmHouse.Commands.Album;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Tests;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FilmHouse.Commands.Test.Album;

[TestFixture]
public class IsExistWithAlbumIdCommandHandlerTest : TransactionalTestBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task IsExistWithAlbumId01()
    {
        var albumId = this.DbContext.Albums.Where(d => d.Title == new AlbumTitleVO("影集A")).Select(d => d.AlbumId).First();
        var result = await this.Mediator.Send(new IsExistWithAlbumIdCommand(albumId));

        Assert.That(result, Is.True);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task IsExistWithAlbumId02()
    {
        var result = await this.Mediator.Send(new IsExistWithAlbumIdCommand(new AlbumIdVO(Guid.NewGuid())));

        Assert.That(result, Is.False);
    }
}
