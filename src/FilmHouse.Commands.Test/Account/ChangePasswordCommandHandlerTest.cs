using FilmHouse.Commands.Account;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
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
        var result = await this.Mediator.Send(new ChangePasswordCommand(new("tonyzhangshi"), new("11223344")));
        Assert.That(result, Is.EqualTo(ChangePasswordStatus.Success));

        var passwordHash = this.DbContext.UserAccounts.Where(d => d.Account.Equals(new("tonyzhangshi"))).Select(_ => _.PasswordHash).First();
        Assert.That(passwordHash.AsPrimitive(), Is.EqualTo(new PasswordHashVO("11223344").ToHash("tonyzhangshi")));
    }

    [Test]
    public async Task ChangePasswordError01()
    {
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
}

