using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FilmHouse.Core.DependencyInjection;

namespace FilmHouse.Data.Core.Services.Configuration
{
    /// <summary>
    /// 缓存从配置管理表获取的信息。
    /// </summary>
    /// <remarks>
    /// 实际使用配置管理信息时请使用<see cref="ISettingProvider"/>接口。
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ServiceRegister(SelfServiceLifetime.Scoped)]
    public interface ISettingProviderCacher
    {
        /// <summary>
        /// 确保配置管理信息的缓存。
        /// </summary>
        void EnsureCache();
    }
}
