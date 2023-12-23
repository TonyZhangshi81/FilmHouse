using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Account;

public record DeleteAccountCommand(AccountNameVO AccountName, PasswordHashVO InputPassword) : IRequest;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    #region Initizalize

    private readonly IRepository<UserAccountEntity> _repo;
    public DeleteAccountCommandHandler(IRepository<UserAccountEntity> repo)
    {
        _repo = Guard.GetNotNull(repo, nameof(IRepository<UserAccountEntity>));
    }

    #endregion Initizalize

    public async Task Handle(DeleteAccountCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<AccountNameVO, ArgumentNullException>(request.AccountName);
        Guard.RequiresNotNull<PasswordHashVO, ArgumentNullException>(request.InputPassword);

        var account = await _repo.GetAsync(d => d.Account == request.AccountName
                                                && d.PasswordHash == new PasswordHashVO(request.InputPassword.ToHash(request.AccountName.AsPrimitive())));

        if (account != null)
        {
            await _repo.DeleteAsync(account.UserId, ct);
        }
    }
}