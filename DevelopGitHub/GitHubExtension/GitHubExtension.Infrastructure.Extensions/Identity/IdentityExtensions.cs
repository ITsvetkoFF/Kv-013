using System.Security.Claims;
using System.Security.Principal;
using GitHubExtension.Infrastructure.Constants;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Infrastructure.Extensions.Identity
{
    public static class IdentityExtensions
    {
        public static Claim GetExternalAccessTokenClaim(this IPrincipal user)
        {
            var claims = user.Identity as ClaimsIdentity;
            return claims.FindFirst(ClaimConstants.ExternalAccessToken);
        }

        public static Claim GetCurrentProjectIdClaim(this IPrincipal user)
        {
            var claims = user.Identity as ClaimsIdentity;
            return claims.FindFirst(ClaimConstants.CurrentProjectId);
        }

        public static string GetExternalAccessToken(this IPrincipal user)
        {
            var claims = user.Identity as ClaimsIdentity;
            return claims.FindFirstValue(ClaimConstants.ExternalAccessToken);
        }

        public static string GetCurrentProjectName(this IPrincipal user)
        {
            var claims = user.Identity as ClaimsIdentity;
            return claims.FindFirstValue(ClaimConstants.CurrentProjectName);
        }

        public static string GetCurrentProjectId(this IPrincipal user)
        {
            var claims = user.Identity as ClaimsIdentity;
            return claims.FindFirstValue(ClaimConstants.CurrentProjectId);
        }

        public static string GetUserId(this IPrincipal user)
        {
            var claims = user.Identity as ClaimsIdentity;
            return claims.GetUserId();
        }
    }
}
