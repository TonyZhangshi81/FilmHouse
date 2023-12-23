using FilmHouse.Commands.Account;
using FilmHouse.Commands.Test.Utils;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace FilmHouse.Commands.Test.Account;

[TestFixture]
public class CreateAccountCommandHandlerTest : TransactionalTestBase
{
    private static readonly RequestIdVO CurrentRequestId = new RequestIdVO(Guid.NewGuid());

    [Test]
    [Arrange(nameof(OnCreateAccountCommandArrange))]
    public async Task DeleteAccount01()
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

        await this.Mediator.Send(new CreateAccountCommand(account));

        Assert.That(this.DbContext.UserAccounts.Where(d => d.RequestId.Equals(CurrentRequestId)).Any(), Is.EqualTo(true));
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

