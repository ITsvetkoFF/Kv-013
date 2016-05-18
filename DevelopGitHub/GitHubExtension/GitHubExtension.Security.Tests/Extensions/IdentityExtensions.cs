using System.Security.Principal;

namespace GitHubExtension.Security.Test.Extensions
{
    public static class IdentityExtensions
    {
        public static IPrincipal SetUserForController(this IPrincipal user, string name)
        {
            var identity = new GenericIdentity(name);
            var principal = new GenericPrincipal(identity, new[] { "user" });
            return principal;
        }
    }
}
