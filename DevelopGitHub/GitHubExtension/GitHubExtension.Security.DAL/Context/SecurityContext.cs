using System.Data.Entity;
using GitHubExtension.Security.DAL.Configuration.Tables;
using GitHubExtension.Security.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using GitHubExtension.Security.StorageModels.Identity;

namespace GitHubExtension.Security.DAL.Context
{
    public class SecurityContext : DbContext, ISecurityContext
    {
        public IDbSet<Repository> Repositories { get; set; }
        public IDbSet<SecurityRole> SecurityRoles { get; set; }
        public IDbSet<IdentityUserClaim> Claims { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<IdentityRole> Roles { get; set; }

        public SecurityContext()
            : base("GitHubExtension")
        {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Add Configuration to DB
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RolesConfiguration());
            modelBuilder.Configurations.Add(new UserClaimsConfiguration());
            modelBuilder.Configurations.Add(new UserLoginsConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
