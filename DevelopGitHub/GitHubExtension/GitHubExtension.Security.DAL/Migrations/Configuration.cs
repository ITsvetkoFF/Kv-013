using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.StorageModels.Identity;

namespace GitHubExtension.Security.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<SecurityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SecurityContext context)
        {
            context.SecurityRoles.AddOrUpdate(r => r.Name, new SecurityRole() { Name = "Admin" });
            context.SecurityRoles.AddOrUpdate(r => r.Name, new SecurityRole() { Name = "Developer" });
            context.SecurityRoles.AddOrUpdate(r => r.Name, new SecurityRole() { Name = "Reviewer" });
            context.SaveChanges();
        }
    }
}