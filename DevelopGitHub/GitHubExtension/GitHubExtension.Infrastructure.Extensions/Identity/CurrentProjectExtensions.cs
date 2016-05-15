using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Infrastructure.Extensions.Identity
{
    public static class CurrentProjectExtensions
    {
        private const string CurrentProjectName = "CurrentProjectName";
        private const string CurrentProjectId = "CurrentProjectId";

        public static string GetCurrentProjectName(this IPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirstValue(CurrentProjectName);
        }

        public static string GetCurrentProjectId(this IPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirstValue(CurrentProjectId);
        }
    }
}
