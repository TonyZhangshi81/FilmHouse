using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Account;

public record CreateAccountCommand(UserAccountEntity UserAccount) : IRequest;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
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
        _repo = repo;
        _currentRequestId = currentRequestId;
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Task Handle(CreateAccountCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AccountNameVO, ArgumentNullException>(request.UserAccount.Account);
        Guard.RequiresNotNull<PasswordHashVO, ArgumentNullException>(request.UserAccount.PasswordHash);

        var dt = DateTime.Now;

        var account = new UserAccountEntity
        {
            RequestId = this._currentRequestId.Get(),
            UserId = new(Guid.NewGuid()),
            Account = request.UserAccount.Account,
            PasswordHash = new(request.UserAccount.PasswordHash.ToHash(request.UserAccount.Account.AsPrimitive())),
            EmailAddress = request.UserAccount.EmailAddress,
            Avatar = request.UserAccount.Avatar,
            Cover = request.UserAccount.Cover,
            IsAdmin = request.UserAccount.IsAdmin,
            LastLoginIp = request.UserAccount.LastLoginIp,
            LastLoginTime = new(dt),
            CreatedOn = new(dt)
        };

        return _repo.AddAsync(account, ct);
    }
}