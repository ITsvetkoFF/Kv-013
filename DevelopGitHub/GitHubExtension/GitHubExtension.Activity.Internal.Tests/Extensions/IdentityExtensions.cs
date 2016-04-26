using System.Security.Claims;
using System.Security.Principal;

namespace GitHubExtension.Activity.Internal.Tests.Extensions
{
    public static class IdentityExtensions
    {
        public static IPrincipal SetUserForController(this IPrincipal user, string name, string typeOfClaim, string valueOfClaim)
        {
            var identity = new GenericIdentity(name);
            identity.AddClaim(new Claim(typeOfClaim, valueOfClaim));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            return principal;
        }
    }
}