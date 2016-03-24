using System.Data.Entity;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace GitHubExtension.Security.WebApi.Library
{
    public static class SimpleInjectorConfiguration
    {
        public static Container ConfigurationSimpleInjector()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // DisposableService implements IDisposable
            container.Register<ISecurityContext>(() => new SecurityContext(), Lifestyle.Scoped);
            container.Register<IGithubService, GithubService>(Lifestyle.Singleton);

            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<SecurityRoleManager>(Lifestyle.Scoped);

            container.Register<IUserStore<User>, GitHubUserStore>(Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>, GitHubRoleStore>(Lifestyle.Scoped);

            container.Register<IAuthService, AuthService>(Lifestyle.Scoped);

            container.Verify();
            return container;
        }
    }
}