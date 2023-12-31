using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Account;

public record CreateAccountCommand(AccountNameVO AccountName, PasswordHashVO Password, LastLoginIpVO clientIP) : IRequest<CreateAccountContect>;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountContect>
{
    #region Initizalize

    private readonly ICurrentRequestId _currentRequestId;
    private readonly IRepository<UserAccountEntity> _repo;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="currentRequestId"></param>
    public CreateAccountCommandHandler(IRepository<UserAccountEntity> repo, ICurrentRequestId currentRequestId)
    {
        this._repo = Guard.GetNotNull(repo, nameof(IRepository<UserAccountEntity>));
        this._currentRequestId = Guard.GetNotNull(currentRequestId, nameof(ICurrentRequestId));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<CreateAccountContect> Handle(CreateAccountCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AccountNameVO, ArgumentNullException>(request.AccountName);
        Guard.RequiresNotNull<PasswordHashVO, ArgumentNullException>(request.Password);
        Guard.RequiresNotNull<LastLoginIpVO, ArgumentNullException>(request.clientIP);

        if (await this._repo.AnyAsync(d => d.Account == request.AccountName))
        {
            return new CreateAccountContect() { Status = CreateStatus.AccountExist };
        }

        var dt = DateTime.Now;
        var account = new UserAccountEntity
        {
            RequestId = this._currentRequestId.Get(),
            UserId = new(Guid.NewGuid()),
            Account = request.AccountName,
            PasswordHash = new(request.Password.ToHash(request.AccountName.AsPrimitive())),
            EmailAddress = new EmailAddressVO($"{request.AccountName.AsPrimitive()}_guest@gmail.com"),
            Avatar = new UserAvatarVO("User_1.jpg"),
            Cover = new CoverVO("Cover_1.jpg"),
            IsAdmin = new IsAdminVO(false),
            LastLoginIp = request.clientIP,
            LastLoginTime = new(dt),
            IsEnabled = new(true),
            CreatedOn = new(dt)
        };
        var user = await _repo.AddAsync(account, ct);

        return new CreateAccountContect() { Status = CreateStatus.Success, UserId = user.UserId, IsAdmin = new(false) };
    }
}

/// <summary>
/// 登录尝试的可能结果
/// </summary>
public enum CreateStatus
{
    // 登陆成功
    Success = 0,
    // 用户名已经存在
    AccountExist = 1,
}

public class CreateAccountContect
{
    public CreateStatus Status { get; set; }
    public UserIdVO UserId { get; set; }
    public IsAdminVO IsAdmin { get; set; }
}