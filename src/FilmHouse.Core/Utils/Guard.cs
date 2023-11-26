using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// <paramref name="value"/>表示不是空。
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="value">値</param>
        /// <param name="propertyName">属性名</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/>为空的情况。</exception>
        /// <returns>空非容许型返回。</returns>
        [return: NotNull]
        public static T GetNotNull<T>(T value, string propertyName)
        {
            if (value == null)
            {
                throw new InvalidOperationException($"{propertyName}没有被初始化。");
            }
            return value;
        }

        /// <summary>
        /// <paramref name="value"/>表示不是空。
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="value">値</param>
        /// <param name="propertyName">属性名</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/>为空的情况。</exception>
        /// <returns>空非容许型返回。</returns>
        [return: NotNull]
        public static T GetNotNull<T>(Nullable<T> value, string propertyName)
            where T : struct
        {
            if (value == null)
            {
                throw new InvalidOperationException($"{propertyName}没有被初始化。");
            }
            return value.Value;
        }

        /// <summary>
        /// 在不符合条件的情况下抛出例外。
        /// </summary>
        /// <typeparam name="TException"><paramref name="condition"/>当为假时被慢放的例外类型</typeparam>
        /// <param name="condition">条件</param>
        public static void Requires<TException>(bool condition)
            where TException : Exception, new()
        {
            if (condition)
            {
                return;
            }
            throw new TException();
        }

        /// <summary>
        /// 在不符合条件的情况下抛出例外。
        /// </summary>
        /// <typeparam name="TException"><paramref name="condition"/>当为假时被慢放的例外类型</typeparam>
        /// <param name="condition">条件</param>
        /// <param name="createExceptionExpression">生成慢速异常的处理</param>
        public static void Requires<TException>(bool condition, Expression<Func<TException>> createExceptionExpression)
            where TException : Exception
        {
            if (condition)
            {
                return;
            }
            throw createExceptionExpression.Compile()();
        }

        /// <summary>
        /// 当值为空时，抛出指定的异常。
        /// </summary>
        /// <typeparam name="T"><paramref name="value"/>类型</typeparam>
        /// <typeparam name="TException"><paramref name="value"/>为空时被抛出的例外类型</typeparam>
        /// <param name="value">检查空的值条件</param>
        public static void RequiresNotNull<T, TException>([NotNull] T value)
            where TException : Exception, new()
        {
            if (value != null)
            {
                return;
            }
            throw new TException();
        }

        /// <summary>
        /// 当值为空时，抛出指定的异常。
        /// </summary>
        /// <typeparam name="T"><paramref name="value"/>类型</typeparam>
        /// <typeparam name="TException"><paramref name="value"/>为空时被抛出的例外类型</typeparam>
        /// <param name="value">检查空的值条件</param>
        /// <param name="createExceptionExpression">生成慢速异常的处理</param>
        public static void RequiresNotNull<T, TException>([NotNull] T value, Expression<Func<TException>> createExceptionExpression)
            where TException : Exception
        {
            if (value != null)
            {
                return;
            }
            throw createExceptionExpression.Compile()();
        }
    }
}