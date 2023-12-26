using FilmHouse.Commands.Celeb;
using FilmHouse.Core.ValueObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FilmHouse.Commands.Test.Celeb;

[TestFixture]
public class IsExistWithCelebrityIdCommandHandlerTest : TransactionalTestBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task IsExistWithCelebrityId01()
    {
        var celebrityId = this.DbContext.Celebrities.Where(d => d.Name == new CelebrityNameVO("蒂姆·波顿")).Select(d => d.CelebrityId).First();
        var result = await this.Mediator.Send(new IsExistWithCelebrityIdCommand(celebrityId));

        Assert.That(result, Is.True);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task IsExistWithCelebrityId02()
    {
        var result = await this.Mediator.Send(new IsExistWithCelebrityIdCommand(new CelebrityIdVO(Guid.NewGuid())));

        Assert.That(result, Is.False);
    }
}
