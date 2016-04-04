using GitHubExtension.Activity.Internal.WebApi.Package;
using GitHubExtension.Security.DAL.Package;
using GitHubExtension.Security.WebApi.Library.Package;
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
                typeof(WebApiActivityPackage).Assembly,
                typeof(DALPackage).Assembly, 
                typeof(WebApiLibraryPackage).Assembly
            });
            container.Verify();

            return container;
        }
    }
}