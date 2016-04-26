using Microsoft.Owin;

namespace GitHubExtension.Infrastructure.Extensions.Owin
{
    public static class AuthenticationExtension
    {
        public static bool IsAuthenticated(this IOwinContext context)
        {
            return context.Authentication.User != null && context.Authentication.User.Identity != null
                   && context.Authentication.User.Identity.IsAuthenticated;
        }
    }
}
