using System.Security.Claims;
using FilmHouse.Commands.Comment;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Tests;
using FilmHouse.Tests.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FilmHouse.Commands.Test.Comment;

[TestFixture]
public class CreateCommandHandlerTest : TransactionalTestBase
{
    private static readonly RequestIdVO CurrentRequestId = new RequestIdVO(Guid.NewGuid());

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Test]
    [TestCase(null, "00000000-0000-0000-0000-000000000001")]
    [TestCase("", "00000000-0000-0000-0000-000000000001")]
    [TestCase("test", "00000000-0000-0000-0000-000000000001")]
    [TestCase("test", "剪刀手安德华")]
    [Arrange(nameof(OnCreateCommandArrange))]
    public async Task CreateCommand01(string content, string movieId)
    {
        if (movieId.Equals("剪刀手安德华"))
        {
            var movFindId = this.DbContext.Movies.Where(d => d.Title == new MovieTitleVO(movieId)).Select(d => d.MovieId).First();
            var commentId = await this.Mediator.Send(new CreateCommand(new ContentVO(content), movFindId));

            var resContent = this.DbContext.Comments.Where(d => d.CommentId == commentId).Select(d => d.Content).First();
            Assert.That(resContent, Dz.EqualTo(content));
        }
        else
        {
            var commentId = await this.Mediator.Send(new CreateCommand(new ContentVO(content), new MovieIdVO(Guid.Parse(movieId))));
            Assert.That(commentId, Is.Null);
        }
    }

    /// <summary>
    /// 未登录游客，无法修改评论
    /// </summary>
    /// <returns></returns>
    [Test]
    [Arrange(nameof(OnCreateCommandArrange))]
    public async Task CreateCommand02()
    {
        var movieId = this.DbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First();

        var commentId = await this.Mediator.Send(new CreateCommand(new ContentVO("test"), movieId));

        Assert.That(commentId, Is.Null);
    }

    /// <summary>
    /// 设置服务时进行独特的定制
    /// </summary>
    /// <param name="services">服务</param>
    /// <param name="mocks"></param>
    private static void OnCreateCommandArrange(IServiceCollection services, ICollection<Mock> mocks)
    {
        var caseMethodName = TestExecutionContext.CurrentContext.CurrentTest.MethodName;

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        switch (caseMethodName)
        {
            case nameof(CreateCommand02):
                // 未登录游客
                httpContextAccessorMock.Setup(h => h.HttpContext.User.Identity.IsAuthenticated).Returns(false);
                break;
            case nameof(CreateCommand01):
            default:
                // 登录游客
                var user = new ClaimsPrincipal(
                            new ClaimsIdentity(
                                    new Claim[] { new Claim(ClaimTypes.Role, "Administrator"), new Claim("uid", Seed.TestUser_tonyzhangshi_UserId.AsPrimitive().ToString()) },
                                    CookieAuthenticationDefaults.AuthenticationScheme));
                httpContextAccessorMock.Setup(h => h.HttpContext.User).Returns(user);
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

        var requestId = new Mock<ICurrentRequestId>();
        requestId.Setup(_ => _.Get()).Returns(CurrentRequestId);
        services.AddTransient(_ => requestId.Object);
    }

}
