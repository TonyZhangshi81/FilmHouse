#nullable enable
namespace FilmHouse.Core.Exceptions;

[Serializable]
public abstract class FilmHouseFatalException : Exception
{
    /// <summary>
    /// <see cref="FilmHouseFatalException"/>
    /// </summary>
    public FilmHouseFatalException()
        : base()
    {
    }

    /// <summary>
    /// <see cref="FilmHouseFatalException"/>
    /// </summary>
    public FilmHouseFatalException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// <see cref="FilmHouseFatalException"/>
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public FilmHouseFatalException(string? message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// <see cref="FilmHouseFatalException"/>
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected FilmHouseFatalException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    {
    }
}
