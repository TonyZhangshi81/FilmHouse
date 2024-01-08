using System.Text;
using FilmHouse.Commands.HttpClients.DeleteComment;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Services.HttpClients;
using FilmHouse.Core.Services.HttpClients.Models;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Comment;

public record DeleteCommand(CommentIdVO CommentId) : IRequest<DeleteCommentStatus>;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, DeleteCommentStatus>
{
    #region Initizalize

    private readonly IRepository<CommentEntity> _comment;
    private readonly ILogger<DeleteCommandHandler> _logger;
    private readonly ICurrentRequestId _currentRequestId;
    private readonly IFilmHouseHttpClientFactory _httpClientFactory;

    public DeleteCommandHandler(IRepository<CommentEntity> comment, ILogger<DeleteCommandHandler> logger, ICurrentRequestId currentRequestId, IFilmHouseHttpClientFactory httpClientFactory)
    {
        this._comment = Guard.GetNotNull(comment, nameof(IRepository<CommentEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<DeleteCommandHandler>));
        this._currentRequestId = Guard.GetNotNull(currentRequestId, nameof(ICurrentRequestId));
        this._httpClientFactory = Guard.GetNotNull(httpClientFactory, nameof(IFilmHouseHttpClientFactory));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DeleteCommentStatus> Handle(DeleteCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<CommentIdVO, ArgumentNullException>(request.CommentId);

        if (!await this.CallDeleteWebApiAsync(request.CommentId, ct))
        {
            return DeleteCommentStatus.UndefinedComment;
        }
        return DeleteCommentStatus.Success;
    }

    private async Task<bool> CallDeleteWebApiAsync(CommentIdVO commentId, CancellationToken ct)
    {
        var isSuccess = false;
        var client = _httpClientFactory.CreateClient("DeleteComment");

        var requestModel = new RequestObject<RequestMetadataModel, RequestDataModel>();
        requestModel.Metadata = new()
        {
            RequestId = this._currentRequestId.Get()
        };
        requestModel.Request = new()
        {
            CommentId = commentId
        };
        var content = new JsonStringContent(requestModel, Encoding.UTF8, "application/json");

        HttpResponseMessage response = null;
        using (var requestMessage = new HttpJsonRequestMessage(HttpMethod.Get, $"?d={Uri.EscapeDataString(requestModel.ToJsonString())}"))
        {
            response = await client.SendAsync(requestMessage, ct);
            response.EnsureSuccessStatusCode();
            var result = response.Content.ToModel<ResponseObject<ResponseMetadataModel, ResponseDataModel>>();
            if (result.Response != null)
            {
                isSuccess = result.Response.IsSuccess;
            }
        }
        return isSuccess;
    }
}

/// <summary>
/// 删除的结果
/// </summary>
public enum DeleteCommentStatus
{
    Success = 0,
    UndefinedComment = 1
}
