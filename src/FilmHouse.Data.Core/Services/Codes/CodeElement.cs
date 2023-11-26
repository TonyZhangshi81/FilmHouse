using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Core.Services.Codes
{
    /// <summary>
    /// 是表示代码值的类。
    /// </summary>
    public class CodeElement
    {
        public CodeGroupVO Group { get; }

        public CodeKeyVO Code { get; }
 
        public CodeValueVO Name { get; }

        public SortOrderVO Order { get; }

        /// <summary>
        /// <see cref="CodeElement"/>的新实例。
        /// </summary>
        /// <param name="group"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="order"></param>
        public CodeElement(CodeGroupVO group, CodeKeyVO code, CodeValueVO name, SortOrderVO order)
        {
            this.Group = group;
            this.Code = code;
            this.Name = name;
            this.Order = order;
        }
    }
}
