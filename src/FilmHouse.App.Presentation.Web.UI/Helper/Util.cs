using FilmHouse.Core.ValueObjects;

namespace FilmHouse.App.Presentation.Web.UI.Helper
{
    public static class Util
    {
        /// <summary>
        /// 显示用文件大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static DisplayResourceSizeVO CalculateToDiscSize(ResourceSizeVO size)
        {
            if (size.AsPrimitive() > 1024 * 1024 * 1024)
            {
                return new($"{size.AsPrimitive() / (1024 * 1024 * 1024)} G");
            }
            else if (size.AsPrimitive() > 1024 * 1024)
            {
                return new($"{size.AsPrimitive() / (1024 * 1024)} M");
            }
            else if (size.AsPrimitive() > 1024)
            {
                return new($"{size.AsPrimitive() / 1024} K");
            }
            else
            {
                return new($"{size.AsPrimitive()} 字节");
            }
        }
    }
}
