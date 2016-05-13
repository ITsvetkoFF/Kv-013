using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace GitHubExtension.Activity.External.Tests.Extensions
{
    public static class IdentityExtensions
    {
        public static void SetUserForController(this ApiController controller, string name, Claim[] claims)
        {
            var identity = new GenericIdentity(name);
            identity.AddClaims(claims);
            var principal = new GenericPrincipal(identity, new[] { "user" });
            controller.User = principal;
        }
    }
}
