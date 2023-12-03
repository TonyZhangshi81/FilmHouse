using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 用于管理对<see cref="DirectoryInfo"/>的扩展方法的类。
    /// </summary>
    public static class DirectoryInfoExtension
    {
        /// <summary>
        /// 复制目录。
        /// </summary>
        /// <param name="source">复制源的目录信息</param>
        /// <param name="destinationPath">复制目的地的目录路径</param>
        public static void CopyTo(this DirectoryInfo source, string destinationPath)
        {
            var destinationDi = new DirectoryInfo(destinationPath);

            if (!destinationDi.Exists)
            {
                destinationDi.Create();
            }

            // 文件的复印件
            foreach (var fi in source.GetFiles())
            {
                // 如果存在同样的文件，经常重写
                fi.CopyTo(Path.Combine(destinationDi.FullName, fi.Name), true);
            }

            // 复制目录(使用递归)
            foreach (var di in source.GetDirectories())
            {
                di.CopyTo(Path.Combine(destinationDi.FullName, di.Name));
            }
        }
    }
}
