using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
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
