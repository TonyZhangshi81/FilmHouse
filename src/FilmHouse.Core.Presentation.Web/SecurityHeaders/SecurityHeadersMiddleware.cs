using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FilmHouse.Core.Presentation.Web.SecurityHeaders;

public sealed class SecurityHeadersMiddleware
{
    private const string PERMISSIONSPOLICYHEADER = "Permissions-Policy";

    private const string CSPHEADER = "Content-Security-Policy";

    private const string XFRAMEOPTIONSHEADER = "X-Frame-Options";

    private const string XXSSPROTECTIONHEADER = "X-XSS-Protection";

    private const string XCONTENTTYPEOPTIONSHEADER = "X-Content-Type-Options";

    private const string REFERRERPOLICYHEADER = "Referrer-Policy";

    private readonly RequestDelegate next;

    private readonly SecurityHeaderOptions options;

    public SecurityHeadersMiddleware(RequestDelegate next, SecurityHeaderOptions options)
    {
        this.next = next;
        this.options = options;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!string.IsNullOrEmpty(this.options.PermissionsPolicyHeader))
        {
            context.Response.Headers.Add(PERMISSIONSPOLICYHEADER, this.options.PermissionsPolicyHeader);
        }

        if (!string.IsNullOrEmpty(this.options.CspHeader))
        {
            context.Response.Headers.Add(CSPHEADER, this.options.CspHeader);
        }

        // 用来防止网页被嵌入到其他网站的框架中，可以将该标头设置为`SAMEORIGIN`，这将只允许网页在同源的框架中加载。
        context.Response.Headers.Add(XFRAMEOPTIONSHEADER, this.options.XFrameOptionsHeader);
        // 用于启用浏览器的内置跨站脚本（XSS）保护机制，`1; mode=block`表示启用该机制，并在检测到潜在的 XSS 攻击时阻止页面加载。
        context.Response.Headers.Add(XXSSPROTECTIONHEADER, "1; mode=block");
        // 为了防止浏览器执行非预期的 MIME 类型的脚本，可以将该标头设置为`nosniff`。这将告诉浏览器不要嗅探响应中的 MIME 类型，而应始终使用服务器指定的 MIME 类型。
        context.Response.Headers.Add(XCONTENTTYPEOPTIONSHEADER, "nosniff");

        if (this.options.ReferrerPolicyHeader != null)
        {
            context.Response.Headers.Add(REFERRERPOLICYHEADER, this.options.ReferrerPolicyHeader);
        }

        await this.next(context);
    }
}
