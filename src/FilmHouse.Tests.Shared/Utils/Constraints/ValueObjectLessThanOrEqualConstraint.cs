﻿using Moq;
using NUnit.Framework.Constraints;

namespace FilmHouse.Tests.Utils.Constraints;

/// <summary>
/// 实施值对象的验证处理。
/// </summary>
public class ValueObjectLessThanOrEqualConstraint : LessThanOrEqualConstraint
{
    private readonly object _expected;

    /// <summary>
    /// <see cref="ValueObjectLessThanOrEqualConstraint"/>的新实例。
    /// </summary>
    /// <param name="expected">期待值</param>
    public ValueObjectLessThanOrEqualConstraint(object expected)
        : base(expected)
    {
        this._expected = expected;
    }

    /// <summary>
    /// 实施验证。
    /// </summary>
    /// <typeparam name="TActual"></typeparam>
    /// <param name="actual"></param>
    /// <returns></returns>
    public override ConstraintResult ApplyTo<TActual>(TActual actual)
    {
        return this.ApplyToCore(actual, this._expected, base.ApplyTo);
    }
}
