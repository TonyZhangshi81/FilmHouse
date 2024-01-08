using FilmHouse.Commands.Account;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Tests;
using FilmHouse.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace FilmHouse.Commands.Test.Account;

[TestFixture]
public class CreateAccountCommandHandlerTest : TransactionalTestBase
{
    private static readonly RequestIdVO CurrentRequestId = new RequestIdVO(Guid.NewGuid());

    /// <summary>
    /// 用户登录成功
    /// </summary>
    /// <returns></returns>
    [Test]
    [Arrange(nameof(OnCreateAccountCommandArrange))]
    public async Task DeleteAccount01()
    {
        var account = new UserAccountEntity()
        {
            Account = new("test99"),
            PasswordHash = new("Password01"),
            EmailAddress = new("tonyzhangshi@163.com"),
            Avatar = new("001.png"),
            Cover = new("002.png"),
            IsAdmin = new(false),
            LastLoginIp = new("111:222:333:444"),
            LastLoginTime = new(DateTime.Now),
            CreatedOn = new(DateTime.Now)
        };

        var result = await this.Mediator.Send(new CreateAccountCommand(account.Account, account.PasswordHash, account.LastLoginIp));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.RequestId.Equals(CurrentRequestId)).Any(), Is.EqualTo(true));
        Assert.That(result.Status, Is.EqualTo(CreateStatus.Success));
        Assert.That(result.UserId, Dz.EqualTo(this.DbContext.UserAccounts.Where(d => d.Account == account.Account).Select(d => d.UserId).First()));
        Assert.That(result.IsAdmin, Dz.EqualTo(false));
    }

    /// <summary>
    /// 用户已经存在
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task DeleteAccount02()
    {
        var account = new UserAccountEntity()
        {
            Account = new("test01"),
            PasswordHash = new("Password01"),
            EmailAddress = new("tonyzhangshi@163.com"),
            Avatar = new("001.png"),
            Cover = new("002.png"),
            IsAdmin = new(false),
            LastLoginIp = new("111:222:333:444"),
            LastLoginTime = new(DateTime.Now),
            CreatedOn = new(DateTime.Now)
        };

        var result = await this.Mediator.Send(new CreateAccountCommand(account.Account, account.PasswordHash, account.LastLoginIp));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.Account.Equals(account.Account)).Any(), Is.EqualTo(true));
        Assert.That(result.Status, Is.EqualTo(CreateStatus.AccountExist));
    }


    /// <summary>
    /// 设置服务时进行独特的定制
    /// </summary>
    /// <param name="services">服务</param>
    private static void OnCreateAccountCommandArrange(IServiceCollection services, ICollection<Mock> mocks)
    {
        Assert.That(services.Count(), Is.GreaterThanOrEqualTo(36));
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


}

