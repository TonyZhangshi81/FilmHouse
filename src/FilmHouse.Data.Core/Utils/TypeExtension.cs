using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 获取这个类型是否是值对象的类型。
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        /// <remarks>
        /// <see cref="IValueObject"/>判定正在实现接口的是值型。
        /// </remarks>
        public static bool IsValueObject(this Type valueType)
        {
            return typeof(IValueObject).IsAssignableFrom(valueType);
        }

        /// <summary>
        /// <paramref name="primitiveValue"/>为原始型<paramref name="valueObjectType" />生成类型的实例。
        /// </summary>
        /// <param name="valueObjectType"></param>
        /// <param name="primitiveValue"></param>
        /// <returns></returns>
        public static object CreateValueObjectInstance(this Type valueObjectType, object primitiveValue)
        {
            if (!IsValueObject(valueObjectType))
            {
                // 原始型的情况下直接回复
                return Converter.ChangeType(primitiveValue, valueObjectType)!;
            }
            var typedValue = valueObjectType.ConvertToPrimitive(primitiveValue);
            var instance = Activator.CreateInstance(valueObjectType, typedValue);
            return instance!;
        }

        /// <summary>
        /// 转换为ValueObject的原始类型的值。
        /// </summary>
        /// <param name="valueObjectType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ConvertToPrimitive(this Type valueObjectType, object value)
        {
            var parameterType = valueObjectType.GetValueObjectPrimitiveType(value);

            try
            {
                if (parameterType == typeof(bool))
                {
                    var booleanConverter = new Utils.BooleanConverter();
                    var typedValue = booleanConverter.ConvertFrom(value);
                    return typedValue!;
                }
                else if (parameterType == typeof(Guid))
                {
                    return new Guid($"{value}");
                }
                else
                {
                    if (value.GetType().IsValueObject())
                    {
                        var typedValue = value.CastTo(parameterType);
                        return typedValue;
                    }
                    else
                    {
                        var typedValue = Converter.ChangeType(value, parameterType);
                        return typedValue!;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ArgumentException($"传递给ValueOject构造器的值 [{value}] 没能转换成 {parameterType.FullName} ", nameof(value), exception);
            }
        }

        /// <summary>
        /// 获得非Nullable的原始类型。如果不是Nullable型，则返回原来的型。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Type UnwrapNullableType(this Type self) => Nullable.GetUnderlyingType(self) ?? self;

        /// <summary>
        /// 获取ValueObject包含的原始类型。
        /// </summary>
        /// <param name="valueObjectType"></param>
        /// <param name="value">指定用于指定构造函数的自变量</param>
        /// <returns></returns>
        public static Type GetValueObjectPrimitiveType(this Type valueObjectType, object value = null)
        {
            if (!valueObjectType.IsValueObject())
            {
                return valueObjectType;
            }

            var ctors = valueObjectType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (ctors.Length == 0)
            {
                throw new ArgumentException("ValueObject需要有一个可以设定一个原始类型的构造器。", nameof(valueObjectType));
            }
            var ctor = ctors.FirstOrDefault(_ => _.GetParameters().Length == 1);
            if (value != null)
            {
                var targetCtor = ctors.Where(_ => _.GetParameters().Length == 1).FirstOrDefault(_ => _.GetParameters()[0].ParameterType == value.GetType());
                if (targetCtor != null)
                {
                    ctor = targetCtor;
                }
            }
            if (ctor == null)
            {
                throw new ArgumentException("ValueObject需要有一个可以设定一个原始类型的构造器。", nameof(valueObjectType));
            }
            var parameterType = ctor.GetParameters()[0].ParameterType;

            return parameterType;
        }
    }
}
