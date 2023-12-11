namespace FilmHouse.Core.Presentation.Web.SecurityHeaders;

public enum ReferrerPolicies
{
    None,

    Empty,

    NoReferrer,

    NoReferrerWhenDowngrade,

    SameOrigin,

    Origin,

    StrictOrigin,

    OriginWhenCrossOrigin,

    StrictOriginWhenCrossOrigin,

    UnsafeUrl
}
