using FilmHouse.Commands.Account;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace FilmHouse.Commands.Test.Account;

[TestFixture]
public class ChangePasswordCommandHandlerTest : TransactionalTestBase
{
    private static readonly RequestIdVO CurrentRequestId = new RequestIdVO(Guid.NewGuid());

    [Test]
    public async Task ChangePassword01()
    {
        var uuid = new RequestIdVO(Guid.NewGuid());
        var sysDate = new CreatedOnVO(System.DateTime.Now);

        await this.DbContext.UserAccounts.AddRangeAsync(this.GetUserAccounts(uuid, sysDate));
        await this.DbContext.SaveChangesAsync();

        var result = await this.Mediator.Send(new ChangePasswordCommand(new("tonyzhangshi"), new("11223344")));
        Assert.That(result, Is.EqualTo(ChangePasswordStatus.Success));

        var passwordHash = this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi"))).Select(_ => _.PasswordHash).First();
        Assert.That(passwordHash.AsPrimitive(), Is.EqualTo(new PasswordHashVO("11223344").ToHash("tonyzhangshi")));
    }

    [Test]
    public async Task ChangePasswordError01()
    {
        var uuid = new RequestIdVO(Guid.NewGuid());
        var sysDate = new CreatedOnVO(System.DateTime.Now);

        await this.DbContext.UserAccounts.AddRangeAsync(this.GetUserAccounts(uuid, sysDate));
        await this.DbContext.SaveChangesAsync();

        var account = new AccountNameVO("tonyzhangshi01");
        var result = await this.Mediator.Send(new ChangePasswordCommand(account, new("11223344")));

        Assert.That(result, Is.EqualTo(ChangePasswordStatus.UndefinedAccount));
        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(account)).Any(), Is.EqualTo(false));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    protected override void SetupServices(IServiceCollection services)
    {
        base.SetupServices(services);

        var requestId = new Mock<ICurrentRequestId>();
        requestId.Setup(_ => _.Get()).Returns(CurrentRequestId);
        services.AddTransient(_ => requestId.Object);

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

