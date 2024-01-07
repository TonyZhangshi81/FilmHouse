using FilmHouse.App.Presentation.Web.Api.Models.Comment.Delete;
using FilmHouse.Core.Services.HttpClients;
using FilmHouse.Core.Services.HttpClients.Models;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.Api.Controllers.Comment;

[ApiController]
[Route("api/comment/[controller]")]
public class DeleteController : ControllerBase
{
    private readonly IRepository<CommentEntity> _comment;
    private readonly ILogger<DeleteController> _logger;

    public DeleteController(IRepository<CommentEntity> repo, ILogger<DeleteController> logger)
    {
        this._comment = Guard.GetNotNull(repo, nameof(IRepository<CommentEntity>));
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

        var isSuccess = false;
        if (await this._comment.AnyAsync(d => d.CommentId == requestModel!.Request!.CommentId))
        {
            await this._comment.DeleteAsync(requestModel!.Request!.CommentId);
            isSuccess = true;
        }

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
