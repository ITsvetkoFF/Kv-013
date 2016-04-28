using System.Net;
using System.Threading.Tasks;

using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Infrastructure.Extensions.Owin;

using Microsoft.Owin;

namespace GitHubExtension.EntryPoint
{
    /// <summary>
    /// Middleware that check external access token presence in claims
    /// </summary>
    public class TokenCheckMiddleware : OwinMiddleware
    {
        private const string NoToken = "You don't have access token to enter this resource";

        public TokenCheckMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (context.IsAuthenticated())
            {
                if (!context.Authentication.User.HasClaim(c => c.Type == ClaimConstants.ExternalAccessToken))
                {
                    context.Authentication.SignOut();
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync(NoToken);
                }
            }

            await Next.Invoke(context);
        }
    }
}