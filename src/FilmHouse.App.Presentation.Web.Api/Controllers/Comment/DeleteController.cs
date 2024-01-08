using FilmHouse.App.Presentation.Web.Api.Models.Comment.Delete;
using FilmHouse.Core.Services.HttpClients;
using FilmHouse.Core.Services.HttpClients.Models;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.Api.Controllers.Comment;

[ApiController]
[Route("api/comment/[controller]")]
public class DeleteController : ControllerBase
{
    private readonly ILogger<DeleteController> _logger;
    private readonly IMediator _mediator;

    public DeleteController(IMediator mediator, ILogger<DeleteController> logger)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<DeleteController>));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetRequestModel request)
    {
        var requestModel = this.GetObject<RequestMetadataModel, RequestDataModel>(request);
        if (!this.ModelState.IsValid)
        {
            return this.CreateInvalidModelStateContent(requestModel);
        }

        var command = new FilmHouse.Api.Commands.Comment.DeleteCommand(requestModel!.Request!.CommentId);
        var result = await this._mediator.Send(command);

        var isSuccess = (result == FilmHouse.Api.Commands.Comment.DeleteCommentStatus.Success);
        var response = new ResponseObject<ResponseMetadataModel, ResponseDataModel>()
        {
            Metadata = new()
            {
                RequestId = requestModel!.Metadata!.RequestId,
                Status = new(System.Net.HttpStatusCode.OK),
                Timestamp = new ApiResponseTimeVO(System.DateTime.Now)
            },
            Response = new()
            {
                IsSuccess = isSuccess
            }
        };
        return this.CreateResponseContent(response);
    }
}
