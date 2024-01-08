#nullable enable
namespace FilmHouse.Core.Exceptions;

[Serializable]
public abstract class FilmHouseErrorException : Exception
{
    /// <summary>
    /// <see cref="FilmHouseErrorException"/>
    /// </summary>
    public FilmHouseErrorException()
        : base()
    {
    }

    /// <summary>
    /// <see cref="FilmHouseErrorException"/>
    /// </summary>
    public FilmHouseErrorException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// <see cref="FilmHouseErrorException"/>
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public FilmHouseErrorException(string? message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// <see cref="FilmHouseErrorException"/>
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected FilmHouseErrorException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    {
    }
}
