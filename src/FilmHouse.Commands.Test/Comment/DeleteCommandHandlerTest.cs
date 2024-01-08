using System.Text;
using FilmHouse.Commands.Comment;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Services.HttpClients;
using FilmHouse.Core.Services.HttpClients.Models;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FilmHouse.Commands.Test.Comment;

[TestFixture]
public class DeleteCommandHandlerTest : TransactionalTestBase
{
    private static readonly RequestIdVO CurrentRequestId = new RequestIdVO(Guid.NewGuid());

    [Test]
    public async Task DeleteCommand01()
    {
        var commentId = this.DbContext.Comments.Where(d => d.UserId == Seed.TestUser_tonyzhangshi_UserId).Select(d => d.CommentId).First();

        var result = await this.Mediator.Send(new DeleteCommand(commentId));
        Assert.That(result, Is.EqualTo(DeleteCommentStatus.Success));
    }

    [Test]
    public async Task DeleteCommand02()
    {
        var result = await this.Mediator.Send(new DeleteCommand(new Core.ValueObjects.CommentIdVO(Guid.Empty)));
        Assert.That(result, Is.EqualTo(DeleteCommentStatus.UndefinedComment));
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

        this.MockHttpClient(services);
    }

    private void MockHttpClient(IServiceCollection services)
    {
        var isuccess = false;
        var caseMethodName = TestExecutionContext.CurrentContext.CurrentTest.MethodName;
        switch (caseMethodName)
        {
            case nameof(this.DeleteCommand01):
                isuccess = true;
                break;
            case nameof(this.DeleteCommand02):
            default:
                isuccess = false;
                break;
        }

        var factory = new Mock<IFilmHouseHttpClientFactory>();
        var client = new Mock<IFilmHouseHttpClient>();

        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        var responseContent = new ResponseObject<ResponseMetadataModel, FilmHouse.Commands.HttpClients.DeleteComment.ResponseDataModel>()
        {
            Response = new()
            {
                IsSuccess = isuccess
            }
        };
        response.Content = new StringContent(responseContent.ToJsonString(), Encoding.UTF8, "application/json");
        client.Setup(_ => _.SendAsync(It.IsAny<HttpJsonRequestMessage>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(response));

        factory.Setup(_ => _.CreateClient("DeleteComment")).Returns(client.Object);
        services.AddTransient(_ => factory.Object);
    }

}

