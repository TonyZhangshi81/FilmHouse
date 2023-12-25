
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Commands.Account;

public record ChangePasswordCommand(AccountNameVO AccountName, PasswordHashVO ClearPassword) : IRequest<ChangePasswordStatus>;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordStatus>
{
    #region Initizalize

    private readonly IRepository<UserAccountEntity> _repo;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;

    public ChangePasswordCommandHandler(IRepository<UserAccountEntity> repo, ILogger<ChangePasswordCommandHandler> logger)
    {
        this._repo = Guard.GetNotNull(repo, nameof(IRepository<UserAccountEntity>));
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<ChangePasswordCommandHandler>));
    }

    #endregion Initizalize

    public async Task<ChangePasswordStatus> Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AccountNameVO, ArgumentNullException>(request.AccountName);

        var account = await this._repo.GetAsync(d => d.Account == request.AccountName);
        if (account is null)
        {
            this._logger.LogError($"LocalAccountEntity with Id '{request.AccountName}' not found.");
            // throw new InvalidOperationException($"LocalAccountEntity with Id '{request.AccountName}' not found.");
            return ChangePasswordStatus.UndefinedAccount;
        }

        account.PasswordHash = new(request.ClearPassword.ToHash(account.Account.AsPrimitive()));
        account.UpDatedOn = new(DateTime.Now);
        await this._repo.UpdateAsync(account, ct);

        return ChangePasswordStatus.Success;
    }
}

/// <summary>
/// 密码变更的可能结果
/// </summary>
public enum ChangePasswordStatus
{
    Success = 0,
    UndefinedAccount = 1
}