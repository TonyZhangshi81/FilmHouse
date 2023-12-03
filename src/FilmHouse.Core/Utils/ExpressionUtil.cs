using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// <see cref="Expression"/>
    /// </summary>
    public static class ExpressionUtil
    {
        /// <summary>
        /// 这是一种方法，用于在异常投掷等情况下获取指示访问类和属性的字符串。
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetExpressionStructure<TValue>(Expression<Func<TValue>> expression)
        {
            return expression.Body.ToString();
        }

        /// <summary>
        /// 取得表达式所表示的成员的名字。
        /// </summary>
        /// <typeparam name="T">表示成员的表达式的返回值的类型。</typeparam>
        /// <param name="expression">表示成员的式子，不能是空的。</param>
        /// <returns>成员的名字。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/>是NULL的情况</exception>
        public static string GetMemberName<T>(Expression<Func<T>> expression)
        {
            var body = expression.Body;
            var memberExpression = body as MemberExpression;
            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }
            var memberName = GetMemberNameRecursive(body);
            return memberName;
        }

        private static string GetMemberNameRecursive(Expression expression)
        {
            var unaryExpression = (UnaryExpression)expression;
            var memberExpression = (MemberExpression)unaryExpression.Operand;
            return memberExpression.Member.Name;
        }
    }
}
