using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Domain.Interfaces;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace GitHubExtension.IoCManager
{
    [assembly: PreApplicationStartMethod(typeof(DependencyConfig), "Initialize")]
    public static class DependencyConfig
    {
        public static void Initialize()
        {
            var container = BuildContainer();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);
        }

        public static Container BuildContainer()
        {
            AreaRegistration.RegisterAllAreas();
            // Optionally verify the container's configuration.
            var container = new Container();

            // Select the scoped lifestyle that is appropriate for the application
            // you are building. For instance:
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            //Allow registration for type IEnumerable<Exception Logger>
            container.Options.ResolveUnregisteredCollections = true;

            // DisposableService implements IDisposable
            container.Register<ISecurityContext, SecurityContext>(Lifestyle.Scoped);
            container.Register<IGithubService, GithubService>(Lifestyle.Transient);
            container.Register<IApplicationUserManager, ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<IUserStore<User>>(() => new UserStore<User>(new SecurityContext()), Lifestyle.Scoped);

            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);

            return container;
        }
    }
}
