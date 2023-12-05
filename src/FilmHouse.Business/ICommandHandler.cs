using System.Threading;
using System.Threading.Tasks;
using FilmHouse.Core.DependencyInjection;

namespace FilmHouse.Business;

[ServiceRegister(SelfServiceLifetime.Scoped)]
public interface ICommandHandler<TCommand, TResult>
{
    Task<TResult> HandleAsync(TCommand command);
}

