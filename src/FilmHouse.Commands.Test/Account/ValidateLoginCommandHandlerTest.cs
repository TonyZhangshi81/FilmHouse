using FilmHouse.Commands.Account;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
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
        var uuid = new RequestIdVO(Guid.NewGuid());
        var sysDate = new CreatedOnVO(System.DateTime.Now);

        await this.DbContext.UserAccounts.AddRangeAsync(this.GetUserAccounts(uuid, sysDate));
        await this.DbContext.SaveChangesAsync();

        var userId = this.DbContext.UserAccounts.Where(d => d.Account.Equals(new(accountName))).Select(d => d.UserId).First();

        LastLoginIpVO lastLoginIp = new("172.168.6.8");
        await this.Mediator.Send(new ValidateLoginCommand(userId, lastLoginIp));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new(accountName)) && d.LastLoginIp.Equals(lastLoginIp)).Any(), Is.EqualTo(true));
    }

    [Test]
    public async Task ValidateLogin02()
    {
        var uuid = new RequestIdVO(Guid.NewGuid());
        var sysDate = new CreatedOnVO(System.DateTime.Now);

        await this.DbContext.UserAccounts.AddRangeAsync(this.GetUserAccounts(uuid, sysDate));
        await this.DbContext.SaveChangesAsync();

        LastLoginIpVO lastLoginIp = new("172.168.6.8");
        await this.Mediator.Send(new ValidateLoginCommand(new(Guid.NewGuid()), lastLoginIp));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi")) && d.LastLoginIp.Equals(new("201.182.1.23"))).Any(), Is.EqualTo(true));
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

