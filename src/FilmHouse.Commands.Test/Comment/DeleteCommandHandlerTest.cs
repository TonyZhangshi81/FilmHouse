using FilmHouse.Commands.Comment;
using NUnit.Framework;

namespace FilmHouse.Commands.Test.Comment;

[TestFixture]
public class DeleteCommandHandlerTest : TransactionalTestBase
{
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
        var result = await this.Mediator.Send(new DeleteCommand(new Core.ValueObjects.CommentIdVO(Guid.Parse("00000000-0000-0000-0000-000000000001"))));
        Assert.That(result, Is.EqualTo(DeleteCommentStatus.UndefinedComment));
    }
}

