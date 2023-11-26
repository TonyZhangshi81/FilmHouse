using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Core.Services.Codes
{
    /// <summary>
    /// 提供从代码管理表获取的信息的接口。
    /// </summary>
    [ServiceRegister(SelfServiceLifetime.Scoped)]
    public interface ICodeProvider
    {
        /// <summary>
        /// 获取基准日有效的对象组的代码信息。
        /// </summary>
        /// <param name="group">代码组</param>
        /// <param name="code">提取的代码值</param>
        /// <returns>有效代码信息</returns>
        CodeContainer AvailableAt(CodeGroupVO group);

        /// <summary>
        /// 获取与代码ID相关联的代码信息。
        /// </summary>
        /// <param name="group">代码组</param>
        /// <param name="code">提取的代码值</param>
        /// <returns></returns>
        CodeElement ElementAt(CodeGroupVO group, CodeKeyVO code);
    }
}
