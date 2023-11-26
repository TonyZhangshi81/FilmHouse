using System.Reflection;

namespace FilmHouse.Web
{
    /// <summary>
    /// 是作为变更设定值的构成的初始对应利用的Setup类。
    /// </summary>
    public class StartupCore
    {
        public StartupCore() { }

        /// <summary>
        /// 列举应用程序的运行路径和下面的文件夹中的配件。
        /// </summary>
        /// <param name="distinct">是否排除重复的组合件文件名</param>
        /// <returns>列举的组合件。列举顺序是按照组合件名称的长短顺序。(为了让FilmHouse系统最先被读取)</returns>
        internal static IEnumerable<Assembly> GetAppAssemblies(bool distinct)
        {
            var rootDir = AppDomain.CurrentDomain.BaseDirectory;
            var rootDi = new DirectoryInfo(rootDir);
            var usedAssemblies = new HashSet<string>();

            var targetDll = $"FilmHouse.***.dll";
            var assemblyFiles = Directory.GetFiles(rootDir, targetDll, SearchOption.TopDirectoryOnly);
            foreach (var assemblyFile in assemblyFiles.OrderBy(_ => _.Length))
            {
                var fi = new FileInfo(assemblyFile);

                // 获取当前已经被读取的一组组件(已经被读取的用于返回而不Load)
                var asm = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(_ => _.FullName != null && _.FullName.StartsWith("FilmHouse."))
                    .Select(_ => new { Assembly = _, FileInfo = new FileInfo(_.Location) })
                    .FirstOrDefault(_ => _.FileInfo.Name == fi.Name)?.Assembly ?? Assembly.LoadFrom(assemblyFile);
                if (usedAssemblies.Contains(fi.Name))
                {
                    continue;
                }
                if (distinct)
                {
                    // 只存储重复去除的情况
                    usedAssemblies.Add(fi.Name);
                }
                yield return asm;
            }
        }
    }
}
