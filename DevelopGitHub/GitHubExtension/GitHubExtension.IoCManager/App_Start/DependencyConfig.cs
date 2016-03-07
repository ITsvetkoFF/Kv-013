using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using GitHubExtension.Domain;
using GitHubExtension.Domain.Interfaces;
using GitHubExtension.Models.StorageModels.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;

namespace GitHubExtension.IoCManager.App_Start
{
    [assembly: PreApplicationStartMethod(typeof(GitHubExtension.IoCManager.App_Start.DependencyConfig), "Initialize")]
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
            var container = new Container();
            var lifestyle = Lifestyle.Singleton;

            container.Register<ISecurityDbContext, SecurityDbContext>(lifestyle);
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(BuildContainer().GetInstance<SecurityDbContext>()));


            container.Verify();

            return container;
        }
    }
}
