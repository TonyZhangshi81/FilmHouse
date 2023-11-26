using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// <see cref="IQueryable{TSource}"/>的类，用于定义扩展方法。
    /// </summary>
    public static class IQueryableExtension
    {
        /// <summary>
        /// 动态(或)连接多个搜索条件的方法
        /// </summary>
        /// <param name="source">过滤处理IQueryable{TSource}</param>
        /// <param name="predicates">检索条件组(必须指定一个以上要素)</param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereOr<TSource>(this IQueryable<TSource> source, params Expression<Func<TSource, bool>>[] predicates)
        {
            // 如果一个搜索条件都没有被指定，则发布例外
            int length = predicates.Length;
            if (length == 0)
            {
                // 不指定检索条件。
                throw new ArgumentException("No search criteria are specified.");
            }

            var param = Expression.Parameter(typeof(TSource));
            // 根据检索条件组的初始检索条件生成表达式
            Expression body = Expression.Invoke(predicates[0], param);
            for (int i = 1; i < predicates.Length; i++)
            {
                // 用(或)连接搜索条件组的要素
                body = Expression.OrElse(body, Expression.Invoke(predicates[i], param));
            }
            // λ表达式(搜索条件)生成
            var lambda = Expression.Lambda<Func<TSource, bool>>(body, param);
            // 调用Where方法，通过生成的lambda表达式对数据进行过滤。
            return source.Where(lambda);
        }

        /// <summary>
        /// 只有<paramref name="condition"/>是true时，才添加<paramref name="predicate"/>作为WHERE子句。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition">设定条件</param>
        /// <param name="predicate">WHERE句的条件式</param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, Expression<Func<bool>> condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition.Compile()())
            {
                return source.Where(predicate);
            }
            else
            {
                return source;
            }
        }

        /// <summary>
        /// 只有<paramref name="conditionValue"/>是true时，才添加<paramref name="predicate"/>作为WHERE子句。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="conditionValue">判断是否为空的值</param>
        /// <param name="predicate">WHERE句的条件式</param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIfNotNull<TSource>(this IQueryable<TSource> source, object conditionValue, Expression<Func<TSource, bool>> predicate)
        {
            if (conditionValue == null)
            {
                return source;
            }
            return source.Where(predicate);
        }

        /// <summary>
        /// 只有当<paramref name="conditionValue"/>不是空或空字符时，才添加<paramref name="predicate"/>作为WHERE子句。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="conditionValue">判断是否是空字符的值</param>
        /// <param name="predicate">WHERE句的条件式</param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIfNotNullAndEmpty<TSource>(this IQueryable<TSource> source, string conditionValue, Expression<Func<TSource, bool>> predicate)
        {
            if (string.IsNullOrEmpty(conditionValue))
            {
                return source;
            }
            return source.Where(predicate);
        }
    }
}
