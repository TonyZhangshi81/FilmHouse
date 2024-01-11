using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Spec;
using MediatR;

namespace FilmHouse.Commands.Album;

public record DisplayCommand() : IRequest<DisplayContect>;

public class DisplayCommandHandler : IRequestHandler<DisplayCommand, DisplayContect>
{
    #region Initizalize

    private readonly IRepository<AlbumEntity> _albums;
    private readonly IRepository<MarkEntity> _marks;

    public DisplayCommandHandler(IRepository<AlbumEntity> albums, IRepository<MarkEntity> marks)
    {
        this._albums = Guard.GetNotNull(albums, nameof(IRepository<AlbumEntity>));
        this._marks = Guard.GetNotNull(marks, nameof(IRepository<MarkEntity>));
    }

    #endregion Initizalize

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DisplayContect> Handle(DisplayCommand request, CancellationToken ct)
    {
        var albums = await this._albums.SelectAsync(new AlbumSpec(), c => c, ct);

        var contect = new DisplayContect();
        foreach (var item in albums)
        {
            var count = await this._marks.CountAsync(d => d.Target == new MarkTargetIdVO(item.AlbumId.AsPrimitive()) && d.Type == MarkTypeVO.Codes.MarkTypeCode7, ct);
            contect.AlbumFollows.Add(new DisplayContect.AlbumFollow()
            {
                AlbumId = item.AlbumId,
                FollowCount = new FollowCountVO(count)
            });
        }

        contect.Albums = albums;

        return contect;
    }
}

public class DisplayContect
{
    public IReadOnlyList<AlbumEntity> Albums { get; set; }
    public List<AlbumFollow> AlbumFollows { get; set; }

    public class AlbumFollow
    {
        public AlbumIdVO AlbumId { get; set; }
        public FollowCountVO FollowCount { get; set; } = 0;
    }


    public DisplayContect()
    {
        AlbumFollows = new List<AlbumFollow>();
    }
}