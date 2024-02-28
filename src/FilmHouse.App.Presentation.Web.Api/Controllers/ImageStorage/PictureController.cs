using FilmHouse.App.Presentation.Web.Api.Models.ImageStorage.Post;
using FilmHouse.Core.Services.HttpClients.Models;
using FilmHouse.Core.Services.MongoBasicOperation;
using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FilmHouse.App.Presentation.Web.Api.Controllers.ImageStorage;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("api/image/[controller]")]
public class PictureController : ControllerBase
{
    /// <summary>
    /// Mongo文件处理
    /// </summary>
    private readonly IMongoFileRepo _mongoFileRepo;

    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="mongoFileRepo">Mongo文件处理</param>
    public PictureController(IMongoFileRepo mongoFileRepo)
    {
        this._mongoFileRepo = mongoFileRepo;
    }

    /// <summary>
    /// 接口上传图片方法
    /// </summary>
    /// <param name="requestModel">文件传输对象,传过来的json数据</param>
    /// <returns>上传结果</returns>
    [HttpPost]
    public async Task<IActionResult> Post(RequestObject<RequestMetadataModel, RequestDataModel> requestModel)
    {
        if (!this.ModelState.IsValid)
        {
            return this.CreateInvalidModelStateContent(requestModel);
        }

        var internalId = ObjectId.Empty;
        using (var fs = new FileStream(requestModel.Request!.FilePath!, FileMode.Open))
        {

            internalId = await this._mongoFileRepo.UploadFileAsync(requestModel.Request.FileName, fs);
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
                InternalId = internalId,
                IsSuccess = true
            }
        };

        return this.CreateResponseContent(response);
    }




}
