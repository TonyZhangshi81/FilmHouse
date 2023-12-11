#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.DependencyInjection
{
    /// <summary>
    /// 是用于获取在当前处理中统一使用的请求ID的接口。
    /// </summary>
    /// <remarks>
    /// 如果是Web的话，获取HttpContext.Items中存储的信息。
    /// </remarks>
    [ServiceRegister(SelfServiceLifetime.Scoped)]
    public interface ICurrentRequestId
    {
        /// <summary>
        /// 取得现在的处理的请求ID。
        /// </summary>
        /// <returns></returns>
        RequestIdVO Get();

        /// <summary>
        /// 获得正在执行该预处理的请求ID。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGet(out RequestIdVO? value);

        /// <summary>
        /// 设置当前处理正在执行的请求ID。
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        void Set(RequestIdVO current);
    }
}
