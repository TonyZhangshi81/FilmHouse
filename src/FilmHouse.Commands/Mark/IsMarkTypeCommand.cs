using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Mark;

public record IsMarkTypeCommand(MarkTargetIdVO TargrtId, UserIdVO UserId, MarkTypeVO MarkType) : IRequest<bool>;

public class IsMarkTypeCommandHandler : IRequestHandler<IsMarkTypeCommand, bool>
{
    #region Initizalize

    private readonly IRepository<MarkEntity> _repo;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="currentRequestId"></param>
    public IsMarkTypeCommandHandler(IRepository<MarkEntity> repo)
    {
        this._repo = Guard.GetNotNull(repo, nameof(IRepository<MarkEntity>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<bool> Handle(IsMarkTypeCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<MarkTargetIdVO, ArgumentNullException>(request.TargrtId);
        Guard.RequiresNotNull<UserIdVO, ArgumentNullException>(request.UserId);
        Guard.RequiresNotNull<MarkTypeVO, ArgumentNullException>(request.MarkType);

        return await this._repo.AnyAsync(d => d.Target == request.TargrtId && d.UserId == request.UserId && d.Type == request.MarkType);
    }
}