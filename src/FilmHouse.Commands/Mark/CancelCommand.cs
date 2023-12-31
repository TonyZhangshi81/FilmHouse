﻿using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using MediatR;

namespace FilmHouse.Commands.Mark;

public record CancelCommand(MarkTargetIdVO TargrtId, UserIdVO UserId, MarkTypeVO MarkType) : IRequest;

public class CancelCommandHandler : IRequestHandler<CancelCommand>
{
    #region Initizalize

    private readonly ICurrentRequestId _currentRequestId;
    private readonly IRepository<MarkEntity> _repo;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="currentRequestId"></param>
    public CancelCommandHandler(IRepository<MarkEntity> repo, ICurrentRequestId currentRequestId)
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
    public Task Handle(CancelCommand request, CancellationToken ct)
    {
        Guard.RequiresNotNull<MarkTargetIdVO, ArgumentNullException>(request.TargrtId);
        Guard.RequiresNotNull<UserIdVO, ArgumentNullException>(request.UserId);
        Guard.RequiresNotNull<MarkTypeVO, ArgumentNullException>(request.MarkType);

        return this._repo.DeleteAsync(this._repo.GetAsync(d => d.Target == request.TargrtId && d.UserId == request.UserId && d.Type == request.MarkType).Result, ct);
    }
}