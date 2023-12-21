using System.ComponentModel;
using FilmHouse.Core.Services.Configuration;

namespace FilmHouse.Core.ValueObjects
{
    public interface ISlicing<TValue> where TValue : notnull
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        TValue AsShort(ISettingProvider provider);

        [EditorBrowsable(EditorBrowsableState.Never)]
        IReadOnlyList<TValue> ToParallel();
    }
}
