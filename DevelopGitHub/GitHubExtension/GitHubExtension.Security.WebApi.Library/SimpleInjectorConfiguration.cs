using System.Data.Entity;
using System.Linq;
using GitHubExtension.Security.DAL;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Package;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Package;
using GitHubExtension.Security.WebApi.Library.Services;
using GitHubExtension.Statistics.WebApi.Package;
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
            Container container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            return RegisterPackages(container);
        }

        public static Container RegisterPackages(Container container)
        {
            container.RegisterPackages(new[]
            {
                typeof(DALPackage).Assembly,
                typeof(WebApiLibraryPackage).Assembly,
                typeof(SecurityWebApiPackage).Assembly
            });
            container.Verify();

            return container;
        }
    }
}