using System.Security.Claims;
using System.Threading.Tasks;

using GitHubExtension.Infrastructure.Constants;

using Owin.Security.Providers.GitHub;

namespace GitHubExtension.Security.WebApi.Provider
{
    public class GitHubAuthProvider : GitHubAuthenticationProvider
    {
        public override Task Authenticated(GitHubAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(ClaimConstants.ExternalAccessToken, context.AccessToken));

            return Task.FromResult<object>(null);
        }
    }
}