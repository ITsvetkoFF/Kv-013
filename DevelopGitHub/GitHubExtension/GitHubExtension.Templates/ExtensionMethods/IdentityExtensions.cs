using System.Security.Claims;
using System.Security.Principal;

using Microsoft.AspNet.Identity;

namespace GitHubExtension.Templates.ExtensionMethods
{
    public static class IdentityExtensions
    {
        private const string CurrentProjectName = "CurrentProjectName";

        public static string GetCurrentProjectName(this IPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirstValue(CurrentProjectName);
        }
    }
}