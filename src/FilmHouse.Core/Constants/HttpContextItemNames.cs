﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FilmHouse.Core.Constants
{
    /// <summary>
    /// <see cref="HttpContext.Items"/>中为了管理要存储的项目的键名的类。
    /// </summary>
    public static partial class HttpContextItemNames
    {
        private const string Prefix = "httpcontext-";

        /// <summary>
        /// 用于存储处理的请求ID的键
        /// </summary>
        public const string CurrentRequestId = Prefix + "request-id";

        /// <summary>
        /// 用于存储webapi-key值的键
        /// </summary>
        public const string ApiKeyRequestHeaderItem = "x-filmhouse-api-key";
    }
}
