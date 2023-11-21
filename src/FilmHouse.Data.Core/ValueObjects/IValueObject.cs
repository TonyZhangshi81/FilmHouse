using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// Value the interface that indicates that it is an object.
    /// </summary>
    /// <remarks>
    /// Types implementing this interface are treated as value objects.
    /// This interface branches the binding process between the screen and the model.
    /// </remarks>
    public interface IValueObject
    {
        /// <summary>
        /// This is a method for getting a primitive type independently of the type.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        object AsPrimitiveObject();
    }
}
