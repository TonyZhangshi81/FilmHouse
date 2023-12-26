using FilmHouse.Commands.Account;
using FilmHouse.Core.ValueObjects;
using NUnit.Framework;

namespace FilmHouse.Commands.Test.Account;

[TestFixture]
public class ValidateLoginCommandHandlerTest : TransactionalTestBase
{
    [Test]
    [TestCase("tonyzhangshi")]
    [TestCase("test01")]
    [TestCase("test02")]
    public async Task ValidateLogin01(string accountName)
    {
        var userId = this.DbContext.UserAccounts.Where(d => d.Account.Equals(new(accountName))).Select(d => d.UserId).First();

        LastLoginIpVO lastLoginIp = new("172.168.6.8");
        await this.Mediator.Send(new ValidateLoginCommand(userId, lastLoginIp));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new(accountName)) && d.LastLoginIp.Equals(lastLoginIp)).Any(), Is.EqualTo(true));
    }

    [Test]
    public async Task ValidateLogin02()
    {
        LastLoginIpVO lastLoginIp = new("172.168.6.8");
        await this.Mediator.Send(new ValidateLoginCommand(new(Guid.NewGuid()), lastLoginIp));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi")) && d.LastLoginIp.Equals(new("201.182.1.23"))).Any(), Is.EqualTo(true));
    }

}

