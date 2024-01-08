using FilmHouse.Commands.Account;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Tests;
using NUnit.Framework;

namespace FilmHouse.Commands.Test.Account;

[TestFixture]
public class PasswordSignInCommandHandlerTest : TransactionalTestBase
{
    [Test]
    public async Task PasswordSignIn01()
    {
        var login = await this.Mediator.Send(new PasswordSignInCommand(new("tonyzhangshi"), new("Tony19811031")));

        var userItem = this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi"))).Select(d => new { d.UserId, d.IsAdmin }).First();

        Assert.That(login.Status, Is.EqualTo(SignInStatus.Success));
        Assert.That(login.UserId, Is.EqualTo(userItem.UserId));
        Assert.That(login.IsAdmin, Is.EqualTo(userItem.IsAdmin));
    }

    [Test]
    public async Task PasswordSignIn02()
    {
        var login = await this.Mediator.Send(new PasswordSignInCommand(new("tonyzhangshi01"), new("Tony19811031")));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi01"))).Any(), Is.EqualTo(false));
        Assert.That(login.Status, Is.EqualTo(SignInStatus.UndefinedAccount));
        Assert.That(login.UserId, Is.EqualTo(new UserIdVO(Guid.Empty)));
        Assert.That(login.IsAdmin, Is.EqualTo(new IsAdminVO(false)));
    }

    [Test]
    public async Task PasswordSignIn03()
    {
        var login = await this.Mediator.Send(new PasswordSignInCommand(new("tonyzhangshi"), new("tony19811031")));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi"))).Any(), Is.EqualTo(true));
        Assert.That(login.Status, Is.EqualTo(SignInStatus.Failure));
        Assert.That(login.UserId, Is.EqualTo(new UserIdVO(Guid.Empty)));
        Assert.That(login.IsAdmin, Is.EqualTo(new IsAdminVO(false)));
    }

}

