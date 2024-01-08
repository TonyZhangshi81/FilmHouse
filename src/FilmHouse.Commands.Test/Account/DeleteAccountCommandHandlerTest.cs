using FilmHouse.Commands.Account;
using FilmHouse.Tests;
using FilmHouse.Tests.Utils;
using NUnit.Framework;

namespace FilmHouse.Commands.Test.Account;

[TestFixture]
public class DisplayCommandHandlerTest : TransactionalTestBase
{
    [Test]
    [TestCase("tonyzhangshi", "Tony19811031")]
    [TestCase("test01", "111111")]
    [TestCase("test02", "222222")]
    public async Task DeleteAccount01(string account, string password)
    {
        await this.Mediator.Send(new DeleteAccountCommand(new(account), new(password)));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new(account))).Select(d => d.IsEnabled).First(), Dz.EqualTo(false));
    }

    [Test]
    [TestCase("tonyzhangshi01", "Tony19811031")]
    [TestCase("tonyzhangshi", "tony19811031")]
    public async Task DeleteAccount02(string account, string password)
    {
        await this.Mediator.Send(new DeleteAccountCommand(new(account), new(password)));

        if (this.DbContext.UserAccounts.Any(d => d.Account == new Core.ValueObjects.AccountNameVO(account)))
        {
            Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new(account))).Select(d => d.IsEnabled).First(), Dz.EqualTo(true));
        }
    }

}

