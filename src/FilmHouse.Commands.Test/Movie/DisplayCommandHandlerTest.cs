using System.Security.Claims;
using FilmHouse.Commands.Movie;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Tests;
using FilmHouse.Tests.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FilmHouse.Commands.Test.Movie;

[TestFixture]
public class DisplayCommandHandlerTest : TransactionalTestBase
{
    private static readonly RequestIdVO CurrentRequestId = new RequestIdVO(Guid.NewGuid());

    /// <summary>
    /// 管理员身份
    /// </summary>
    /// <returns></returns>
    [Test]
    [Arrange(nameof(OnDisplayCommandArrange))]
    public async Task DiosplayMovie01()
    {
        var movieId = this.DbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First();
        var result = await this.Mediator.Send(new DisplayCommand(movieId));

        Assert.That(result, Is.Not.Null);
        // 评论数量
        Assert.That(result.CommentCount, Is.EqualTo(7));
        // 被收录的影集数量
        Assert.That(result.Albums.Count, Is.EqualTo(2));
        Assert.That(result.IsCreate, Is.True);
        Assert.That(result.IsPlan, Is.True);
        Assert.That(result.IsFinish, Is.True);
        Assert.That(result.IsFavor, Is.True);
        Assert.That(result.PlanCount, Is.EqualTo(5));
        Assert.That(result.FinishCount, Is.EqualTo(6));
        Assert.That(result.FavorCount, Is.EqualTo(7));
        Assert.That(result.PersonalReview, Is.Not.Null);

        // 个人评点
        var personalReviewContent = this.DbContext.Comments.Where(d => d.MovieId == movieId && d.UserId == Seed.TestUser_tonyzhangshi_UserId).Select(d => d.Content).First();
        Assert.That(result.PersonalReview.Content, Is.EqualTo(personalReviewContent));
    }

    /// <summary>
    /// 非管理员身份
    /// </summary>
    /// <returns></returns>
    [Test]
    [Arrange(nameof(OnDisplayCommandArrange))]
    public async Task DiosplayMovie02()
    {
        var movieId = this.DbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First();

        var result = await this.Mediator.Send(new DisplayCommand(movieId));

        Assert.That(result.IsCreate, Is.False);
    }

    /// <summary>
    /// 用户未登陆状态
    /// </summary>
    /// <returns></returns>
    [Test]
    [Arrange(nameof(OnDisplayCommandArrange))]
    public async Task DiosplayMovie03()
    {
        var movieId = this.DbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First();

        var result = await this.Mediator.Send(new DisplayCommand(movieId));

        Assert.That(result.IsCreate, Is.False);
    }

    /// <summary>
    /// 设置服务时进行独特的定制
    /// </summary>
    /// <param name="services">服务</param>
    /// <param name="mocks"></param>
    private static void OnDisplayCommandArrange(IServiceCollection services, ICollection<Mock> mocks)
    {
        var caseMethodName = TestExecutionContext.CurrentContext.CurrentTest.MethodName;

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        switch (caseMethodName)
        {
            case nameof(DiosplayMovie02):
                // 登陆嘉宾
                var user01 = new ClaimsPrincipal(
                            new ClaimsIdentity(
                                    new Claim[] { new Claim(ClaimTypes.Role, "Guest"), new Claim("uid", Seed.TestUser_tonyzhangshi_UserId.AsPrimitive().ToString()) },
                                    CookieAuthenticationDefaults.AuthenticationScheme));
                httpContextAccessorMock.Setup(h => h.HttpContext.User).Returns(user01);
                break;
            case nameof(DiosplayMovie03):
                // 未登录游客
                httpContextAccessorMock.Setup(h => h.HttpContext.User.Identity.IsAuthenticated).Returns(false);
                break;
            case nameof(DiosplayMovie01):
            default:
                // 管理员
                var user02 = new ClaimsPrincipal(
                            new ClaimsIdentity(
                                    new Claim[] { new Claim(ClaimTypes.Role, "Administrator"), new Claim("uid", Seed.TestUser_tonyzhangshi_UserId.AsPrimitive().ToString()) },
                                    CookieAuthenticationDefaults.AuthenticationScheme));
                httpContextAccessorMock.Setup(h => h.HttpContext.User).Returns(user02);
                break;
        }
        services.AddTransient(_ => httpContextAccessorMock.Object);
        mocks.Add(httpContextAccessorMock);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    protected override void SetupServices(IServiceCollection services)
    {
        base.SetupServices(services);

        var settingProvider = new Mock<ISettingProvider>();
        settingProvider.Setup(_ => _.GetValue(It.IsAny<ConfigKeyVO>())).Returns(new ConfigValueVO("6"));
        services.AddTransient(_ => settingProvider.Object);
    }
}

