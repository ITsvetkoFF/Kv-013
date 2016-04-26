using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Interfaces;

using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Infrastructure
{
    public class GitHubUserStore : UserStore<User>
    {
        public GitHubUserStore(ISecurityContext securityContext)
            : base((SecurityContext)securityContext)
        {
        }
    }
}