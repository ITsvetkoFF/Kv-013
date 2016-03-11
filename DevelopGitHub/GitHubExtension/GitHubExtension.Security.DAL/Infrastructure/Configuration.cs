

namespace GithubExtension.Security.DAL.Infrastructure
{
    //internal sealed class Configuration : DbMigrationsConfiguration<SecurityContext>
    //{
    //    public Configuration()
    //    {
    //        AutomaticMigrationsEnabled = false;
    //    }

    //    protected override void Seed(SecurityContext context)
    //    {
    //        //  This method will be called after migrating to the latest version.

    //        var manager = new UserManager<User>(new UserStore<User>(new SecurityContext()));

    //        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SecurityContext()));

    //        var user = new User()
    //        {
    //            UserName = "SuperPowerUser",
    //            Email = "taiseer.joudeh@gmail.com",
    //            EmailConfirmed = true
              
    //        };

    //        manager.Create(user, "MySuperP@ss!");

    //        if (roleManager.Roles.Count() == 0)
    //        {
    //            roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
    //            roleManager.Create(new IdentityRole { Name = "Admin" });
    //            roleManager.Create(new IdentityRole { Name = "User" });
    //        }

    //        var adminUser = manager.FindByName("SuperPowerUser");

    //        manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });
    //    }
    //}
}
