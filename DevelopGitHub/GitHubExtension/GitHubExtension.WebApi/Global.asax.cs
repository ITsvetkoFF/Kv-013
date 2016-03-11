using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using GitHubExtension.Domain;
using GitHubExtension.Domain.Interfaces;
using GitHubExtension.Identity.Managers;
using GitHubExtension.IoCManager;
using GitHubExtension.IoCManager.App_Start;
using GitHubExtension.Models.StorageModels.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using Container = SimpleInjector.Container;

namespace GitHubExtension.WebApi
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {          
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            // Optionally verify the container's configuration.
            var container = new Container();

            // Select the scoped lifestyle that is appropriate for the application
            // you are building. For instance:
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            //Allow registration for type IEnumerable<Exception Logger>
            container.Options.ResolveUnregisteredCollections = true;

            container.Register<ApplicationUserManager, ApplicationUserManager>(Lifestyle.Scoped);

            // DisposableService implements IDisposable
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new SecurityDbContext()), Lifestyle.Scoped);

            // DisposableService implements IDisposable
            container.Register<ISecurityDbContext, SecurityDbContext>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();


            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}