using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Mark;

public record CreateMarkCommand(MarkTargetIdVO TargrtId, UserIdVO UserId, MarkTypeVO MarkType) : IRequest;

public class CreateMarkCommandHandler : IRequestHandler<CreateMarkCommand>
{
    #region Initizalize

    private readonly ICurrentRequestId _currentRequestId;
    private readonly IRepository<MarkEntity> _repo;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="currentRequestId"></param>
    public CreateMarkCommandHandler(IRepository<MarkEntity> repo, ICurrentRequestId currentRequestId)
    {
        _repo = Guard.GetNotNull(repo, nameof(IRepository<MarkEntity>));
        _currentRequestId = Guard.GetNotNull(currentRequestId, nameof(ICurrentRequestId));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Task Handle(CreateMarkCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<MarkTargetIdVO, ArgumentNullException>(request.TargrtId);
        Guard.RequiresNotNull<UserIdVO, ArgumentNullException>(request.UserId);
        Guard.RequiresNotNull<MarkTypeVO, ArgumentNullException>(request.MarkType);

        var dt = DateTime.Now;

        var mark = new MarkEntity
        {
            RequestId = this._currentRequestId.Get(),
            MarkId = new(Guid.NewGuid()),
            Type = request.MarkType,
            UserId = request.UserId,
            Target = request.TargrtId,
            CreatedOn = new(dt)
        };

        return _repo.AddAsync(mark, ct);
    }
}