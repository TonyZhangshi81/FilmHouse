using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.Services.Configuration
{
    /// <summary>
    /// 提供从配置管理表获取的信息的接口。
    /// </summary>
    [ServiceRegister(SelfServiceLifetime.Scoped)]
    public interface ISettingProvider
    {
        /// <summary>
        /// 按照KEY值提供其相对应的VALUE。
        /// </summary>
        /// <param name="key">配置KEY</param>
        /// <param name="defValue"></param>
        /// <returns>VALUE值</returns>
        ConfigValueVO GetValue(ConfigKeyVO key);

        /// <summary>
        /// 按照KEY值提供其相对应的VALUE。
        /// </summary>
        /// <param name="key">配置KEY</param>
        /// <returns>VALUE值</returns>
        ConfigValueVO GetValue(string key);

        /// <summary>
        /// 按照KEY值提供其相对应的VALUE。
        /// </summary>
        /// <param name="key">配置KEY</param>
        /// <returns>VALUE值</returns>
        IEnumerable<ConfigValueVO> GetValue(ConfigKeyVO[] key);
    }
}
