using GitHubExtension.Security.DAL.Package;
using GitHubExtension.Security.WebApi.Library.Package;
using GitHubExtension.Templates.WebApi.Package;
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
                typeof(WebApiTemplatesPackage).Assembly
            });
            container.Verify();

            return container;
        }
    }
}