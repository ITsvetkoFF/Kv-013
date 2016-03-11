using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using GitHubExtension.Security.DAL.Entities;

namespace GitHubExtension.Security.DAL.Context
{
    // TODO: Check internal
    public class SecurityContext : IdentityDbContext<User>
    {
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<SecurityRole> SecurityRoles { get; set; }
         
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
