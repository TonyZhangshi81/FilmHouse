using FilmHouse.Commands.Account;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.Api.Controllers.Exercise;

[ApiController]
[Route("api/[controller]")]
public class NginxController : ControllerBase
{
    private readonly IRepository<UserAccountEntity> _repo;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;
    private readonly ICodeProvider _codeProvider;

    public NginxController(IRepository<UserAccountEntity> repo, ILogger<ChangePasswordCommandHandler> logger, ICodeProvider codeProvider)
    {
        this._repo = Guard.GetNotNull(repo, nameof(IRepository<UserAccountEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<ChangePasswordCommandHandler>));
        this._codeProvider = Guard.GetNotNull(codeProvider, nameof(ICodeProvider));
    }

    [HttpGet]
    public string Get()
    {

        var codeContainer = this._codeProvider.AvailableAt(CodeGroupVO.Codes.Language);
        foreach (var item in codeContainer.Elements)
        {
            this._logger.LogInformation($"{item.Code}:{item.Name}");
        }

        return $"{HttpContext.Connection.LocalIpAddress!.ToString()}:{HttpContext.Connection.LocalPort.ToString()}";
    }
}
