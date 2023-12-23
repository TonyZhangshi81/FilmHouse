using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Account;

public record ValidateLoginCommand(UserIdVO UserId, LastLoginIpVO IpAddress) : IRequest;

public class ValidateLoginCommandHandler : IRequestHandler<ValidateLoginCommand>
{
    #region Initizalize

    private readonly IRepository<UserAccountEntity> _repo;

    public ValidateLoginCommandHandler(IRepository<UserAccountEntity> repo) 
    {
        _repo = Guard.GetNotNull(repo, nameof(IRepository<UserAccountEntity>));
    }

    #endregion Initizalize

    public async Task Handle(ValidateLoginCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<UserIdVO, ArgumentNullException>(request.UserId);
        Guard.RequiresNotNull<LastLoginIpVO, ArgumentNullException>(request.IpAddress);

        var account = await _repo.GetAsync(request.UserId, ct);
        if (account is not null)
        {
            var dt = DateTime.Now;

            account.LastLoginIp = request.IpAddress;
            account.LastLoginTime = new(dt);
            await _repo.UpdateAsync(account, ct);
        }
    }
}