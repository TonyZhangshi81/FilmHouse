
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Account;

public record ChangePasswordCommand(AccountNameVO AccountName, PasswordHashVO ClearPassword) : IRequest<ChangePasswordStatus>;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordStatus>
{
    #region Initizalize

    private readonly IRepository<UserAccountEntity> _repo;

    public ChangePasswordCommandHandler(IRepository<UserAccountEntity> repo) => _repo = repo;

    #endregion Initizalize

    public async Task<ChangePasswordStatus> Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        var account = await _repo.GetAsync(d => d.Account == request.AccountName);
        if (account is null)
        {
            // throw new InvalidOperationException($"LocalAccountEntity with Id '{request.AccountName}' not found.");
            return ChangePasswordStatus.UndefinedAccount;
        }

        account.PasswordHash = new(request.ClearPassword.ToHash(account.Account.AsPrimitive()));
        account.UpDatedOn = new(DateTime.Now);
        await _repo.UpdateAsync(account, ct);

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