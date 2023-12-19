using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Account;

public record DeleteAccountCommand(AccountNameVO AccountName, PasswordHashVO InputPassword) : IRequest;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IRepository<UserAccountEntity> _repo;
    public DeleteAccountCommandHandler(IRepository<UserAccountEntity> repo) => _repo = repo;

    public async Task Handle(DeleteAccountCommand request, CancellationToken ct)
    {
        var account = await _repo.GetAsync(d => d.Account == request.AccountName && d.PasswordHash == new PasswordHashVO(request.InputPassword.ToHash(request.AccountName.AsPrimitive())));
        if (account != null)
        {
            await _repo.DeleteAsync(account.UserId, ct);
        }
    }
}