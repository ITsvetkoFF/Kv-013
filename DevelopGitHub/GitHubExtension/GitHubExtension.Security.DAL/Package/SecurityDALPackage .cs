using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Security.DAL.Package
{
    public class SecurityDALPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ISecurityContext>(() => new SecurityContext(), Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<SecurityRoleManager>(Lifestyle.Scoped);
            container.Register<IUserStore<User>, GitHubUserStore>(Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>, GitHubRoleStore>(Lifestyle.Scoped);
        }
    }
}
