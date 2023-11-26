using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.Utils;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Core.Services.Codes
{
    /// <summary>
    /// 代表代码组的类。
    /// </summary>
    public class CodeContainer
    {
        /// <summary>
        /// 获取代码组。
        /// </summary>
        public CodeGroupVO Group { get; }

        /// <summary>
        /// 获取代码。
        /// </summary>
        public IReadOnlyList<CodeElement> Elements { get; }

        /// <summary>
        /// <see cref="CodeContainer"/>的新实例。
        /// </summary>
        /// <param name="group"></param>
        /// <param name="elements"></param>
        public CodeContainer(CodeGroupVO group, params CodeElement[] elements)
        {
            this.Group = group;
            this.Elements = new List<CodeElement>(elements);
        }

        /// <summary>
        /// 返回被指定的<paramref name="code"/>的有效代码。
        /// </summary>
        /// <param name="code">提取的代码值</param>
        /// <returns></returns>
        public CodeContainer AvailableAt(CodeKeyVO code = null)
        {
            var destination = new CodeContainer(
                group: this.Group,
                elements: this.Elements
                                .WhereIfNotNull(code, _ => _.Code == code)
                                .OrderBy(_ => _.Order)
                                .ToArray());
            return destination;
        }

    }
}
