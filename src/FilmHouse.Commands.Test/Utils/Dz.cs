using FilmHouse.Commands.Test.Utils.Constraints;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace FilmHouse.Commands.Test.Utils
{
    /// <summary>
    /// 用于对值对象实施验证的辅助类，类似NUnit中的<see cref="Is"/>或<see cref="Does"/>。
    /// </summary>
    public abstract class Dz
    {
        /// <summary>
        /// 对价值对象实施=验证。
        /// </summary>
        /// <param name="expected">期待值</param>
        /// <returns></returns>
        public static EqualConstraint EqualTo(object expected)
        {
            return new ValueObjectEqualConstraint(expected);
        }

        /// <summary>
        /// 实施值对象≤验证。
        /// </summary>
        /// <param name="expected">期待值</param>
        /// <returns></returns>
        public static LessThanOrEqualConstraint LessThanOrEqualTo(object expected)
        {
            return new ValueObjectLessThanOrEqualConstraint(expected);
        }

        /// <summary>
        /// 对≥值对象实施验证。
        /// </summary>
        /// <param name="expected">期待值</param>
        /// <returns></returns>
        public static GreaterThanOrEqualConstraint GreaterThanOrEqualTo(object expected)
        {
            return new ValueObjectGreaterThanOrEqualConstraint(expected);
        }
    }
}
