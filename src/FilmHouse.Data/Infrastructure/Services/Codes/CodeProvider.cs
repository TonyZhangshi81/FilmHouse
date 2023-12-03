using FilmHouse.Core.Utils;
using FilmHouse.Data;
using FilmHouse.Data.Core.Services.Codes;
using FilmHouse.Data.Core.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace FilmHouse.Data.Infrastructure.Services.Codes
{
    /// <summary>
    /// 用于管理从代码管理表获取的信息的类。
    /// </summary>
    public class CodeProvider : ICodeProvider, ICodeProviderCacher
    {
        private readonly IMemoryCache _cache;
        private readonly FilmHouseDbContext _dbcontext;
        private const string CacheKey = nameof(CodeProvider);

        /// <summary>
        /// <see cref="CodeProvider"/>的新实例。
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="cache"></param>
        public CodeProvider(FilmHouseDbContext dbContext, IMemoryCache cache)
        {
            this._dbcontext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// 确保代码管理信息的缓存。
        /// </summary>
        void ICodeProviderCacher.EnsureCache()
        {
            _ = this.GetAllData();
        }

        private (IReadOnlyDictionary<CodeGroupVO, CodeContainer>, int) GetAllData()
        {
            {
                // 如果缓存中存在数据
                if (this._cache.TryGetValue<Tuple<IReadOnlyDictionary<CodeGroupVO, CodeContainer>, int>>(CacheKey, out var entry))
                {
                    return (entry.Item1, entry.Item2);
                }
            }

            lock (CacheKey)
            {
                var groups = new Dictionary<CodeGroupVO, CodeContainer>();
                var results = this._dbcontext.CodeMast.OrderBy(_ => _.Group).ThenBy(_ => _.Order).ToList();
                foreach (var datum in results)
                {
                    var items = new List<CodeElement>();
                    if (!groups.ContainsKey(datum.Group))
                    {
                        items = results.Where(d => d.Group == datum.Group)
                                       .OrderBy(d => d.Order)
                                       .Select(d => new CodeElement(group: d.Group, code: d.Code, name:d.Name, order: d.Order))
                                       .ToList();

                        var codeGroup = new CodeContainer(group: datum.Group, elements: items.ToArray());
                        groups.Add(key: datum.Group, value: codeGroup);
                    }
                }

                // 确保一天的信息
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(24));

                var entry = new Tuple<IReadOnlyDictionary<CodeGroupVO, CodeContainer>, int>(groups, results.Count);
                this._cache.Set(CacheKey, entry, cacheEntryOptions);
                return (groups, results.Count);
            }
        }

        /// <summary>
        /// 作为对象的代码类别，在那个基准日取得有效的代码。
        /// </summary>
        /// <param name="group">代码组</param>
        /// <returns>有效的代码</returns>
        public CodeContainer AvailableAt(CodeGroupVO group)
        {
            var (groups, _) = this.GetAllData();
            return groups[group];
        }

        /// <summary>
        /// <paramref name="id"/>的代码信息。
        /// </summary>
        /// <param name="group">代码组</param>
        /// <param name="code">提取的代码值</param>
        /// <returns>返回代码信息。如果没有找到相应的代码信息，则返回空。</returns>
        /// <exception cref="KeyNotFoundException">在没有找到对应的代码管理信息的情况下被抛出。</exception>
        public CodeElement ElementAt(CodeGroupVO group, CodeKeyVO code)
        {
            var (groups, number) = this.GetAllData();
            var elements = groups[group].AvailableAt(code).Elements;
            Guard.Requires(
                elements.Count == 0,
                () => new KeyNotFoundException($"传递的参数[{nameof(group)}:{nameof(code)}]在{number}件代码管理信息中没有找到对应的信息。"));
            return elements.First();
        }
    }
}
