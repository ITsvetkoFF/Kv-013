using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using GitHubExtension.Security.DAL.Entities;

namespace GithubExtension.Security.DAL.Context
{
    // TODO: Check internal
    public interface ISecurityContext
    {
        IDbSet<Repository> Repositories { get; set; }
        IDbSet<SecurityRole> SecurityRoles { get; set; }
        IDbSet<IdentityUserClaim> Claims { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
    }

    public class SecurityContext : IdentityDbContext<User>, ISecurityContext
    {
        public IDbSet<Repository> Repositories { get; set; }
        public IDbSet<SecurityRole> SecurityRoles { get; set; }
        public IDbSet<IdentityUserClaim> Claims { get; set; }

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
