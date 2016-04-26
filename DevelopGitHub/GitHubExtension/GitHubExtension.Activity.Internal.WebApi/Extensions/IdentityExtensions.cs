using System.Security.Claims;
using System.Security.Principal;

namespace GitHubExtension.Activity.Internal.WebApi.Extensions
{
    public static class IdentityExtensions
    {
        private const string CurrentProjectId = "CurrentProjectId";

        public static Claim GetCurrentProjectClaim(this IPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            Claim currProjectClaim = identity.FindFirst(CurrentProjectId);

            return currProjectClaim;
        }
    }
}