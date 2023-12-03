using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 是用于密码散列化的实用程序类。
    /// </summary>
    public static class IPasswordHashableExtension
    {
        /// <summary>
        /// 进行密码散列化。
        /// </summary>
        /// <typeparam name="T">表示散列化前的密码的类型</typeparam>
        /// <param name="value">散列化前的密码</param>
        /// <param name="soltstring">用作salt的用户ID字符串等字符串。请分配给<see cref="string"/></param>
        /// <returns></returns>
        public static string ToHash<T>(this IPasswordHashable<T> value, string soltstring)
        {
            // 使用使用用户ID字符串等的salt。
            byte[] salt = Encoding.UTF8.GetBytes(soltstring);

            // Pbkdf2方法可以指定散列化的重复次数和创建散列的长度。
            // 由OS选择最优化的实现，比自己实现性能更好。
            byte[] hash = KeyDerivation.Pbkdf2(
              value.AsPrimitive(),
              salt,
              prf: KeyDerivationPrf.HMACSHA256,
              iterationCount: 10000,        // 重复次数
              numBytesRequested: 256 / 8);  // 散列的长度

            // 把哈希值转换成十六进制字符串
            return string.Concat(hash.Select(b => $"{b:x2}"));
        }
    }
}
