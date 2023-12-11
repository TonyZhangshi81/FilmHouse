using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FilmHouse.Core.DependencyInjection;

namespace FilmHouse.Core.Services.Codes
{
    /// <summary>
    /// 只是缓存从代码管理表获取的信息的界面。
    /// </summary>
    /// <remarks>
    /// 实际使用代码管理信息时请使用<see cref="ICodeProvider"/>接口。
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ServiceRegister(SelfServiceLifetime.Scoped)]
    public interface ICodeProviderCacher
    {
        /// <summary>
        /// 确保代码管理信息的缓存。
        /// </summary>
        void EnsureCache();
    }
}
