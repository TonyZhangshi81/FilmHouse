#nullable enable
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// <see cref="string"/>是对原始型的值对象的支援方法。
    /// </summary>
    public static class IStringValueExtension
    {
        /// <summary>
        /// <paramref name="value"/>判断是否包含。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains(this IValue<string>? self, string value) => self != null && self.AsPrimitive().Contains(value);

        /// <summary>
        /// <paramref name="value"/>来判断是否是开头的文字。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool StartsWith(this IValue<string>? self, string value) => self != null && self.AsPrimitive().StartsWith(value);

        /// <summary>
        /// <paramref name="value"/>为结尾的文字。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EndsWith(this IValue<string>? self, string value) => self != null && self.AsPrimitive().EndsWith(value);

        /// <summary>
        /// <paramref name="value"/>为结尾的文字。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Concat<T>(this T? self, string value) where T : IValue<string>, IValueObject
            => (T)(typeof(T).CreateValueObjectInstance(self != null ? new string(self.AsPrimitive().Concat(value).ToArray()) : value));

        /// <summary>
        /// 返回指定长度的新字符串，在当前字符串的开头有空白或指定Unicode字符。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static T? PadLeft<T>(this T? self, int totalWidth) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().PadLeft(totalWidth)) : null);

        /// <summary>
        /// 返回指定长度的新字符串，在当前字符串的开头有空白或指定Unicode字符。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="totalWidth"></param>
        /// <param name="paddingChar"></param>
        /// <returns></returns>
        public static T? PadLeft<T>(this T? self, int totalWidth, char paddingChar) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().PadLeft(totalWidth, paddingChar)) : null);

        /// <summary>
        /// 返回指定长度的新字符串，在当前字符串的末尾空白或嵌入指定Unicode字符。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static T? PadRight<T>(this T? self, int totalWidth) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().PadRight(totalWidth)) : null);

        /// <summary>
        /// 返回指定长度的新字符串，在当前字符串的末尾空白或嵌入指定Unicode字符。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="totalWidth"></param>
        /// <param name="paddingChar"></param>
        /// <returns></returns>
        public static T? PadRight<T>(this T? self, int totalWidth, char paddingChar) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().PadRight(totalWidth, paddingChar)) : null);

        /// <summary>
        /// 获取实例的字符长度。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int? Length<T>(this T? self) where T : IValue<string>, IValueObject
            => (self != null ? self.AsPrimitive().Length : null);

        /// <summary>
        /// 返回将当前实例中出现的所有指定字符串替换为另指定字符串的新字符串。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static T? Replace<T>(this T? self, string oldValue, string? newValue) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().Replace(oldValue, newValue)) : null);

        /// <summary>
        /// 删除当前字符串开头和末尾的所有空白字符。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T? Trim<T>(this T? self) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().Trim()) : null);

        /// <summary>
        /// 删除当前字符串中字符开头和末尾的所有实例。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="trimCahr"></param>
        /// <returns></returns>
        public static T? Trim<T>(this T? self, char trimCahr) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().Trim(trimCahr)) : null);

        /// <summary>
        /// 删除当前字符串末尾的所有空白字符。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T? TrimEnd<T>(this T? self) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().TrimEnd()) : null);

        /// <summary>
        /// 从当前字符串中删除末尾的所有实例。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="trimCahr"></param>
        /// <returns></returns>
        public static T? TrimEnd<T>(this T? self, char trimCahr) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().TrimEnd(trimCahr)) : null);

        /// <summary>
        /// 删除当前字符串开头的所有空白字符。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T? TrimStart<T>(this T? self) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().TrimStart()) : null);

        /// <summary>
        /// 从当前字符串中删除所有开头的实例。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="trimCahr"></param>
        /// <returns></returns>
        public static T? TrimStart<T>(this T? self, char trimCahr) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().TrimStart(trimCahr)) : null);

        /// <summary>
        /// 从实例中获取部分字符串。部分字符串从字符串中指定的字符位置开始，一直到字符串的末尾。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static T? Substring<T>(this T? self, int startIndex) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().Substring(startIndex)) : null);

        /// <summary>
        /// 从实例中获取部分字符串。这个部分字符串是从指定的字符位置开始，指定的字符数的字符串。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T? Substring<T>(this T? self, int startIndex, int length) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().Substring(startIndex, length)) : null);

        /// <summary>
        /// 把这个字符串的副本转换成大写并返回。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T? ToUpper<T>(this T? self) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().ToUpper()) : null);

        /// <summary>
        /// 把这个字符串的副本转换成小写返回。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T? ToLower<T>(this T? self) where T : IValue<string>, IValueObject
            => (T?)(self != null ? typeof(T).CreateValueObjectInstance(self.AsPrimitive().ToLower()) : null);
    }
}
