using GitHubExtension.Domain.Interfaces;
using GitHubExtension.Security.DAL.Context;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Infrastructure
{
    public class GitHubRoleStore : RoleStore<IdentityRole>
    {
        public GitHubRoleStore(ISecurityContext context)
            : base((SecurityContext)context)
        {
        }
    }
}