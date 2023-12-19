using FilmHouse.Commands.Account;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using NUnit.Framework;

namespace FilmHouse.Commands.Test.Account;

[TestFixture]
public class PasswordSignInCommandHandlerTest : TransactionalTestBase
{
    [Test]
    public async Task PasswordSignIn01()
    {
        var uuid = new RequestIdVO(Guid.NewGuid());
        var sysDate = new CreatedOnVO(System.DateTime.Now);

        await this.DbContext.UserAccounts.AddRangeAsync(this.GetUserAccounts(uuid, sysDate));
        await this.DbContext.SaveChangesAsync();

        var login = await this.Mediator.Send(new PasswordSignInCommand(new("tonyzhangshi"), new("Tony19811031")));

        var userItem = this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi"))).Select(d => new { d.UserId, d.IsAdmin }).First();

        Assert.That(login.Status, Is.EqualTo(SignInStatus.Success));
        Assert.That(login.UserId, Is.EqualTo(userItem.UserId));
        Assert.That(login.IsAdmin, Is.EqualTo(userItem.IsAdmin));
    }

    [Test]
    public async Task PasswordSignIn02()
    {
        var uuid = new RequestIdVO(Guid.NewGuid());
        var sysDate = new CreatedOnVO(System.DateTime.Now);

        await this.DbContext.UserAccounts.AddRangeAsync(this.GetUserAccounts(uuid, sysDate));
        await this.DbContext.SaveChangesAsync();

        var login = await this.Mediator.Send(new PasswordSignInCommand(new("tonyzhangshi01"), new("Tony19811031")));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi01"))).Any(), Is.EqualTo(false));
        Assert.That(login.Status, Is.EqualTo(SignInStatus.UndefinedAccount));
        Assert.That(login.UserId, Is.EqualTo(new UserIdVO(Guid.Empty)));
        Assert.That(login.IsAdmin, Is.EqualTo(new IsAdminVO(false)));
    }

    [Test]
    public async Task PasswordSignIn03()
    {
        var uuid = new RequestIdVO(Guid.NewGuid());
        var sysDate = new CreatedOnVO(System.DateTime.Now);

        await this.DbContext.UserAccounts.AddRangeAsync(this.GetUserAccounts(uuid, sysDate));
        await this.DbContext.SaveChangesAsync();

        var login = await this.Mediator.Send(new PasswordSignInCommand(new("tonyzhangshi"), new("tony19811031")));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi"))).Any(), Is.EqualTo(true));
        Assert.That(login.Status, Is.EqualTo(SignInStatus.Failure));
        Assert.That(login.UserId, Is.EqualTo(new UserIdVO(Guid.Empty)));
        Assert.That(login.IsAdmin, Is.EqualTo(new IsAdminVO(false)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private IEnumerable<UserAccountEntity> GetUserAccounts(RequestIdVO uuid, CreatedOnVO dateTime)
    {
        return new List<UserAccountEntity>
       {
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new AccountNameVO("tonyzhangshi"), PasswordHash = new PasswordHashVO(new PasswordHashVO("Tony19811031").ToHash("tonyzhangshi")), EmailAddress = new("tonyzhangshi@163.com"), Avatar = new("0ACFC82E7D5A41FC8AB8FD4EF603C858Tony.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), LastLoginIp = new("201.182.1.23"), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new AccountNameVO("test01"), PasswordHash = new PasswordHashVO(new PasswordHashVO("111111").ToHash("test01")), EmailAddress = new("test01@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new AccountNameVO("test02"), PasswordHash = new PasswordHashVO(new PasswordHashVO("222222").ToHash("test02")), EmailAddress = new("test02@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(true), CreatedOn = dateTime },
       };
    }

}

