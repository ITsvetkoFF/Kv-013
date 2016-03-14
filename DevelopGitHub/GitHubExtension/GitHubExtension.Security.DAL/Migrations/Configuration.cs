namespace GitHubExtension.Security.DAL.Migrations
{
    using GitHubExtension.Security.DAL.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GithubExtension.Security.DAL.Context.SecurityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GithubExtension.Security.DAL.Context.SecurityContext context)
        {

            context.SecurityRoles.AddOrUpdate(r => r.Name, new SecurityRole() {Name = "Admin"});
            context.SecurityRoles.AddOrUpdate(r => r.Name, new SecurityRole() { Name = "Developer" });
            context.SecurityRoles.AddOrUpdate(r => r.Name, new SecurityRole() { Name = "Reviewer" });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
