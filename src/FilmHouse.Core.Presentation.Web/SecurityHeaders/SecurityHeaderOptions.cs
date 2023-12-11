namespace FilmHouse.Core.Presentation.Web.SecurityHeaders;

public sealed class SecurityHeaderOptions
{
    public string PermissionsPolicyHeader { get; set; }

    public string CspHeader { get; set; }

    public string XFrameOptionsHeader { get; set; }

    public string ReferrerPolicyHeader { get; set; }
}
