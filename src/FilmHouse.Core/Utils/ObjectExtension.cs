using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.Utils
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="castToType"></param>
        public static object CastTo(this object self, Type castToType)
        {
            if (!self.GetType().IsValueObject())
            {
                return Converter.ChangeType(self, castToType)!;
            }

            var valueObject = (IValueObject)self;
            if (!castToType.IsValueObject())
            {
                return Converter.ChangeType(valueObject.AsPrimitiveObject(), castToType)!;
            }
            return castToType.CreateValueObjectInstance(valueObject.AsPrimitiveObject());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        public static T CastTo<T>(this object self)
        {
            return (T)CastTo(self, typeof(T));
        }
    }
}
