using System.Data.Entity;

using GitHubExtension.Security.DAL.Configuration.Tables;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Interfaces;

using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Context
{
    public class SecurityContext : DbContext, ISecurityContext
    {
        public SecurityContext()
            : base("GitHubExtension")
        {
        }

        public IDbSet<IdentityUserClaim> Claims { get; set; }

        public IDbSet<Repository> Repositories { get; set; }

        public IDbSet<IdentityRole> Roles { get; set; }

        public IDbSet<SecurityRole> SecurityRoles { get; set; }

        public IDbSet<UserRepositoryRole> UserRepository { get; set; }

        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RolesConfiguration());
            modelBuilder.Configurations.Add(new UserClaimsConfiguration());
            modelBuilder.Configurations.Add(new UserLoginsConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}