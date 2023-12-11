using System.Linq;
using FilmHouse.Core.Utils;
using FilmHouse.Data;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace FilmHouse.Data.Infrastructure.Services.Configuration
{
    /// <summary>
    /// 用于管理从代码管理表获取的信息的类。
    /// </summary>
    public class SettingProvider : ISettingProvider, ISettingProviderCacher
    {
        private readonly IMemoryCache _cache;
        private readonly IRepository<ConfigurationEntity> _configuration;
        private const string CacheKey = nameof(SettingProvider);

        /// <summary>
        /// <see cref="SettingProvider"/>的新实例。
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="cache"></param>
        public SettingProvider(IRepository<ConfigurationEntity> configuration, IMemoryCache cache)
        {
            this._configuration = configuration;
            this._cache = cache;
        }

        /// <summary>
        /// 确保配置管理信息的缓存。
        /// </summary>
        void ISettingProviderCacher.EnsureCache()
        {
            _ = this.GetAllData();
        }

        private IReadOnlyDictionary<ConfigKeyVO, ConfigValueVO> GetAllData()
        {
            {
                // 如果缓存中存在数据
                if (this._cache.TryGetValue<IReadOnlyDictionary<ConfigKeyVO, ConfigValueVO>>(CacheKey, out var entry))
                {
                    return entry;
                }
            }

            lock (CacheKey)
            {
                var config = new Dictionary<ConfigKeyVO, ConfigValueVO>();

                var results = this._configuration.Select(c => c);

                foreach (var item in results)
                {
                    config.Add(key: item.Key, value: item.Value);
                }

                // 确保一天的信息
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(24));
                this._cache.Set(CacheKey, config, cacheEntryOptions);
                return config;
            }
        }

        /// <summary>
        /// 按照KEY值提供其相对应的VALUE。
        /// </summary>
        /// <param name="key">配置KEY</param>
        /// <returns>VALUE值</returns>
        public ConfigValueVO GetValue(ConfigKeyVO key)
        {
            var config = this.GetAllData();
            return config[key];
        }

        /// <summary>
        /// 按照KEY值提供其相对应的VALUE。
        /// </summary>
        /// <param name="key">配置KEY</param>
        /// <returns>VALUE值</returns>
        public ConfigValueVO GetValue(string key)
        {
            return this.GetValue(new ConfigKeyVO(key));
        }

        /// <summary>
        /// 按照KEY值提供其相对应的VALUE。
        /// </summary>
        /// <param name="key">配置KEY</param>
        /// <returns>VALUE值</returns>
        public IEnumerable<ConfigValueVO> GetValue(ConfigKeyVO[] keys)
        {
            var config = this.GetAllData();
            var search = config.Where(d => keys.Contains(d.Key)).Select(d => d.Value).ToArray();
            return search;
        }
    }
}
