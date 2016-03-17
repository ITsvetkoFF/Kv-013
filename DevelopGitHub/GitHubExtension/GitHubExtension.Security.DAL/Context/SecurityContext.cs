using System.Data.Entity;
using GitHubExtension.Domain.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using GitHubExtension.Security.StorageModels.Identity;

namespace GitHubExtension.Security.DAL.Context
{
    public class SecurityContext : IdentityDbContext<User>, ISecurityContext
    {
        public IDbSet<Repository> Repositories { get; set; }
        public IDbSet<SecurityRole> SecurityRoles { get; set; }
        public IDbSet<IdentityUserClaim> Claims { get; set; }
        public IDbSet<Client> Clients { get; set; }

        public SecurityContext()
            : base("GitHubExtension")
        {

        }

        static SecurityContext()
        {
            Database.SetInitializer(new SecurityDbInit());
        }

        public static SecurityContext Create()
        {
            return new SecurityContext();
        }
    }

    public class SecurityDbInit : DropCreateDatabaseIfModelChanges<SecurityContext>
    {
        protected override void Seed(SecurityContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(SecurityContext context)
        {
            // initial configuration will go here
        }
    }
}
