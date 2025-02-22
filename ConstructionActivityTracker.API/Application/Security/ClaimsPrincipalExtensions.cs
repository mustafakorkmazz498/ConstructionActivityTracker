using System.Collections.Immutable;
using System.Security.Claims;

namespace Application.Security;

public static class ClaimsPrincipalExtensions
{
    public static ICollection<string>? GetClaims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        return claimsPrincipal?.FindAll(claimType)?.Select((Claim x) => x.Value).ToImmutableArray();
    }

    public static ICollection<string>? GetRoleClaims(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.GetClaims("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
    }

    public static string? GetIdClaim(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
    }
}
