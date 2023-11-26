using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 管理<see cref="IEnumerable{T}"/>和<see cref="IEnumerable"/>的扩展方法的类。
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 只有在<paramref name="conditionValue"/>不是空的情况下，才追加<paramref name="predicate"/>作为WHERE子句。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="conditionValue">判断是否为空的值</param>
        /// <param name="predicate">WHERE句的条件式</param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIfNotNull<TSource>(this IEnumerable<TSource> source, object conditionValue, Func<TSource, bool> predicate)
        {
            if (conditionValue == null)
            {
                return source;
            }
            return source.Where(predicate);
        }

        /// <summary>
        /// 从非非专利型的<see cref="IEnumerable"/>型转换成<see cref="IEnumerable{T}"/>型。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<T> AsEnumerable<T>(this IEnumerable value)
        {
            foreach (var val in value)
            {
                yield return (T)val;
            }
        }
    }
}
