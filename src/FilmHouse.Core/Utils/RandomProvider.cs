using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace FilmHouse.Core.Utils;

/// <summary>
/// 用于生成不依赖于机器和线程的随机值的类。
/// </summary>
public static class RandomProvider
{
    [SuppressMessage("Style", "IDE0044:添加只读修饰符。", Justification = "<保留中>")]
    private static ThreadLocal<Random> RandomWrapper = new(() =>
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var buffer = new byte[sizeof(int)];
            rng.GetBytes(buffer);
            var seed = BitConverter.ToInt32(buffer, 0);
            return new Random(seed);
        }
    });

    /// <summary>
    /// 获取<see cref="Random"/>实例以生成不依赖于机器和线程的随机值。
    /// </summary>
    /// <returns></returns>
    public static Random GetThreadRandom() => RandomWrapper.Value!;
}
