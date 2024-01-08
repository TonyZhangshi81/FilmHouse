using FilmHouse.Commands.Ask;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Tests;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FilmHouse.Commands.Test.Ask;

[TestFixture]
public class IsExistWithAskIdCommandHandlerTest : TransactionalTestBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task IsExistWithAskId01()
    {
        var askId = this.DbContext.Asks.Where(d => d.Note == new NoteVO("tonyzhangshi请求-雷神4：爱与雷霆")).Select(d => d.AskId).First();
        var result = await this.Mediator.Send(new IsExistWithAskIdCommand(askId));

        Assert.That(result, Is.True);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task IsExistWithAskId02()
    {
        var result = await this.Mediator.Send(new IsExistWithAskIdCommand(new AskIdVO(Guid.NewGuid())));

        Assert.That(result, Is.False);
    }
}
