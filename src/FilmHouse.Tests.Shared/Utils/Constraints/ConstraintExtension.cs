using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using NUnit.Framework.Constraints;

namespace FilmHouse.Tests.Utils.Constraints;

/// <summary>
/// 
/// </summary>
public static class ConstraintExtension
{
    /// <summary>
    /// 实施验证。
    /// </summary>
    /// <typeparam name="TActual"></typeparam>
    /// <param name="constraint"></param>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <param name="baseApplyTo"></param>
    /// <returns></returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:删除未使用的参数。", Justification = "<保留中>")]
    public static ConstraintResult ApplyToCore<TActual>(this Constraint constraint, TActual actual, object expected, Func<object, ConstraintResult> baseApplyTo)
    {
        var actualValue = actual as IValueObject;
        var expectedValue = expected as IValueObject;

        // 如果两个值都不是值对象或值对象，执行既定方法
        if (actualValue == null && expectedValue == null || actualValue != null && expectedValue != null)
        {
            return baseApplyTo(actual);
        }

        // 如果只有实际值是值对象，从实际值的AsPrimitive()中获取原始类型，进行类型转换来验证
        if (actualValue != null && expectedValue == null)
        {
            var value = actualValue.AsPrimitiveObject();
            if (value.GetType() == expected.GetType())
            {
                return baseApplyTo(value);
            }
            try
            {
                return baseApplyTo(value.CastTo(expected.GetType()));
            }
            catch
            {
                return baseApplyTo(value);
            }
        }

        // 因为没有假设只有期望值是值对象的情况，所以执行既定方法
        return baseApplyTo(actual);
    }
}
