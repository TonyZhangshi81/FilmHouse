using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;

namespace FilmHouse.Commands.Account;

public record PasswordSignInCommand(AccountNameVO AccountName, PasswordHashVO InputPassword) : IRequest<SignInContect>;

public class PasswordSignInCommandHandler : IRequestHandler<PasswordSignInCommand, SignInContect>
{
    #region Initizalize

    private readonly IRepository<UserAccountEntity> _repo;

    public PasswordSignInCommandHandler(IRepository<UserAccountEntity> repo) => _repo = repo;

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<SignInContect> Handle(PasswordSignInCommand request, CancellationToken ct)
    {
        var userAccount = await this._repo.GetAsync(d => d.Account == request.AccountName);

        if (userAccount == null)
        {
            return new SignInContect() { UserId = new(Guid.Empty), Status = SignInStatus.UndefinedAccount, IsAdmin = new(false) };
        }
        else if (userAccount.PasswordHash.Equals(request.InputPassword.ToHash(userAccount.Account.AsPrimitive())))
        {
            return new SignInContect() { UserId = userAccount.UserId, Status = SignInStatus.Success, IsAdmin = userAccount.IsAdmin };
        }
        else
        {
            return new SignInContect() { UserId = new(Guid.Empty), Status = SignInStatus.Failure, IsAdmin = new(false) };
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



