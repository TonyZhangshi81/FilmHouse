using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;

namespace FilmHouse.Commands.Account;

public record PasswordSignInCommand(AccountNameVO account, PasswordVO password) : IRequest<SignInContect>;

public class PasswordSignInCommandHandler : IRequestHandler<PasswordSignInCommand, SignInContect>
{
    #region Initizalize

    private readonly IRepository<UserAccountEntity> _account;

    public PasswordSignInCommandHandler(IRepository<UserAccountEntity> account)
    {
        this._account = account;
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<SignInContect> Handle(PasswordSignInCommand request, CancellationToken ct)
    {
        var accountSpec = new UserAccountSpec(request.account);
        var userAccounts = await this._account.SelectAsync(accountSpec, c => c, ct: ct);

        if (!userAccounts.Any())
        {
            return new SignInContect() { UserId = new UserIdVO(Guid.Empty), Status = SignInStatus.UndefinedAccount, IsAdmin = new IsAdminVO(false) };
        }
        else if (userAccounts.ElementAt(0).Password.AsPrimitive() == request.password.ToHash(userAccounts.ElementAt(0).Account.AsPrimitive()))
        {
            return new SignInContect() { UserId = userAccounts.ElementAt(0).UserId, Status = SignInStatus.Success, IsAdmin = userAccounts.ElementAt(0).IsAdmin };
        }
        else
        {
            return new SignInContect() { UserId = new UserIdVO(Guid.Empty), Status = SignInStatus.Failure, IsAdmin = new IsAdminVO(false) };
        }
    }
}

/// <summary>
/// 登录尝试的可能结果
/// </summary>
public enum SignInStatus
{
    // 登陆成功
    Success = 0,
    // 用户名不存在
    UndefinedAccount = 1,
    // 登陆失败
    Failure = 2
}

public class SignInContect
{
    public SignInStatus Status { get; set; }
    public UserIdVO UserId { get; set; }
    public IsAdminVO IsAdmin { get; set; }
}



