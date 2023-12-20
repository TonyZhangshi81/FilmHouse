using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.Commands.Test
{
    public partial class TestBase
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static IEnumerable<ConfigurationEntity> GetInitConfigurationSettings(RequestIdVO uuid, CreatedOnVO dateTime) =>
            new List<ConfigurationEntity>
            {
            new() { RequestId = uuid, Key = new("WebSiteSettings:Name"), Value = new("DEMO"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:SubTitle"), Value = new("DEMO"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:Version"), Value = new("0.2.0.0"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:WebpagesEnabled"), Value = new("false"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:ClientValidationEnabled"), Value = new("true"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:UnobtrusiveJavaScriptEnabled"), Value = new("false"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("Home:Discovery:MaxPage"), Value = new("6"), CreatedOn = dateTime },
            };

        /// <summary>
        /// 代码信息管理表
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static IEnumerable<CodeMastEntity> GetInitCodeMastSettings(RequestIdVO uuid, CreatedOnVO dateTime) =>
            new List<CodeMastEntity>
            {
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("001"), Name = new CodeValueVO("剧情"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("002"), Name = new CodeValueVO("爱情"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("003"), Name = new CodeValueVO("奇幻"), Order = new SortOrderVO(3), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("004"), Name = new CodeValueVO("惊悚"), Order = new SortOrderVO(4), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("005"), Name = new CodeValueVO("喜剧"), Order = new SortOrderVO(5), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("006"), Name = new CodeValueVO("动作"), Order = new SortOrderVO(6), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("007"), Name = new CodeValueVO("科幻"), Order = new SortOrderVO(7), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("008"), Name = new CodeValueVO("冒险"), Order = new SortOrderVO(8), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("009"), Name = new CodeValueVO("悬疑"), Order = new SortOrderVO(9), CreatedOn  = dateTime },

            new() { RequestId = uuid, Group = new CodeGroupVO("Language"), Code = new CodeKeyVO("001"), Name = new CodeValueVO("英语"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("Language"), Code = new CodeKeyVO("002"), Name = new CodeValueVO("法语"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("Language"), Code = new CodeKeyVO("003"), Name = new CodeValueVO("意大利语"), Order = new SortOrderVO(3), CreatedOn  = dateTime },

            new() { RequestId = uuid, Group = new CodeGroupVO("Country"), Code = new CodeKeyVO("001"), Name = new CodeValueVO("美国"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("Country"), Code = new CodeKeyVO("002"), Name = new CodeValueVO("澳大利亚"), Order = new SortOrderVO(2), CreatedOn  = dateTime },

            };
    }
}
