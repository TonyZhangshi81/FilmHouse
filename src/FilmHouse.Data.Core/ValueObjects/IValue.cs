using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// It is an interface for getting a value from a value object.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IValue<TValue> : IValueObject
        where TValue : notnull
    {
        /// <summary>
        /// This is a method for getting a primitive type.
        /// </summary>
        /// <returns></returns>
        TValue AsPrimitive();
    }
}
