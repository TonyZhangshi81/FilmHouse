#nullable enable
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Presentation.Web.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FilmHouse.Core.Presentation.Web.DependencyInjection
{
    /// <summary>
    /// 現在の処理で統一的に使用されるリクエストIDを取得するためのインターフェースです。
    /// </summary>
    /// <remarks>
    /// この型を使用することで、現在の処理がどのリクエストIDとなっているかのかを取得します。
    /// HttpContext.Itemsに格納された情報を取得します。
    /// </remarks>
    public class CurrentRequestId : ICurrentRequestId
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// <see cref="CurrentRequestId"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public CurrentRequestId(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// <see cref="IHttpContextAccessor"/>から<see cref="HttpContext"/>を取得します。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="HttpContext"/>のインスタンスが取得できない場合にスローされます。</exception>
        private HttpContext CurrentContext
        {
            get
            {
                var httpContext = Guard.GetNotNull(this._httpContextAccessor.HttpContext, nameof(HttpContext));
                return httpContext;
            }
        }

        /// <summary>
        /// 現在の処理のリクエストIDを取得します。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"><see cref="HttpContext.Items"/>に想定されているキーが格納されていない場合にスローされます。</exception>
        public RequestIdVO Get()
        {
            var key = HttpContextItemNames.CurrentRequestId;
            var value = this.CurrentContext.Items[key];
            if (value == null)
            {
                throw new KeyNotFoundException($"The key {key} is not stored in HttpContext.Items.");
            }
            if (value is not RequestIdVO)
            {
                throw new NotSupportedException($"The key {key} stored in HttpContext.Items is not of type {typeof(RequestIdVO).Name}.");
            }

            return (RequestIdVO)value;
        }

        /// <summary>
        /// 現在の処理のリクエストIDを取得します。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGet(out RequestIdVO? value)
        {
            value = default(RequestIdVO);
            var val = this.CurrentContext.Items[HttpContextItemNames.CurrentRequestId];
            if (val == null)
            {
                return false;
            }
            if (val is not RequestIdVO)
            {
                return false;
            }
            value = (RequestIdVO)val;
            return true;
        }

        /// <summary>
        /// 現在の処理のリクエストIDを設定します。
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="InvalidOperationException">><see cref="HttpContext.Items"/>にすでに登録されている状態で、2回目以降の登録が発生した場合にスローされます。</exception>
        public void Set(RequestIdVO value)
        {
            var key = HttpContextItemNames.CurrentRequestId;
            if (this.CurrentContext.Items[key] != null)
            {
                throw new InvalidOperationException($"The key {key} stored in HttpContext.Items is already registered. You cannot register twice.");
            }
            this.CurrentContext.Items[key] = value;
        }
    }
}
