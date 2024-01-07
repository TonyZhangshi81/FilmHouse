using System.Text;
using System.Text.Json;
using FilmHouse.Core.Services.HttpClients;
using FilmHouse.Core.Services.HttpClients.Models;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FilmHouse.App.Presentation.Web.Api.Controllers;

public static class ControllerBaseExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMetadata"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="self"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static RequestObject<TMetadata, TRequest>? GetObject<TMetadata, TRequest>(this ControllerBase self, GetRequestModel request)
         where TMetadata : IRequestMetadata
         where TRequest : class
    {
        if (request.Data == null)
        {
            return null;
        }
        var options = JsonSerializerOptionsFactory.GetWebInstance();
        RequestObject<TMetadata, TRequest>? model = null;
        try
        {
            model = JsonSerializer.Deserialize<RequestObject<TMetadata, TRequest>>(request.Data, options);
        }
        catch (Exception exception)
        {
            self.ModelState.AddModelError("", exception.Message);
            return null;
        }

        model = Guard.GetNotNull(model, nameof(model));
        return model!;
    }

    public static IActionResult CreateInvalidModelStateContent<TMetadata>(
        this ControllerBase self,
        IRequestObject<TMetadata>? request,
        System.Net.HttpStatusCode status = System.Net.HttpStatusCode.BadRequest)
            where TMetadata : IRequestMetadata
    {
        return self.CreateInvalidModelStateContent<TMetadata, object>(request, status: status);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMetadata"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="self"></param>
    /// <param name="request"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public static IActionResult CreateInvalidModelStateContent<TMetadata, TResponse>(this ControllerBase self, IRequestObject<TMetadata>? request, System.Net.HttpStatusCode status = System.Net.HttpStatusCode.BadRequest)
        where TMetadata : IRequestMetadata
        where TResponse : class, new()
    {
        //ILogger logger = self.HttpContext.RequestServices.GetRequiredService<ILogger<ControllerBase>>();

        IRequestMetadata? requestMetadata = null;
        if (request != null)
        {
            requestMetadata = request.Metadata;
        }

        var errorResponse = new ResponseObject<ResponseMetadataModel, object>();
        errorResponse.Metadata = new()
        {
            RequestId = requestMetadata?.RequestId ?? new(Guid.Empty),
            Status = new(status),
            Timestamp = new ApiResponseTimeVO(System.DateTime.Now),
            Errors = new List<MessageTextVO>(),
        };

        foreach (var entry in self.ModelState.Values.Where(_ => _.ValidationState == ModelValidationState.Invalid))
        {
            foreach (var error in entry.Errors)
            {
                errorResponse.Metadata.Errors.Add(error.ErrorMessage);
            }
        }
        return self.CreateResponseContent(errorResponse);
    }

    public static IActionResult CreateResponseContent<TMetadata, TResponse>(this ControllerBase self, ResponseObject<TMetadata, TResponse> response)
        where TMetadata : IResponseMetadata
        where TResponse : class
    {
        //ILogger logger = self.HttpContext.RequestServices.GetRequiredService<ILogger<ControllerBase>>();

        var contentResult = self.Content(response.ToJsonString(), "application/json", new UTF8Encoding(false));
        contentResult.StatusCode = (int)response.Metadata!.Status!;

        return contentResult;
    }

}