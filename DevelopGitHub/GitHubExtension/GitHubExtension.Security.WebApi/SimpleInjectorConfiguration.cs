using GitHubExtension.Security.DAL.Package;
using GitHubExtension.Security.WebApi.Package;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace GitHubExtension.Security.WebApi
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
                typeof(DalPackage).Assembly,
                typeof(WebApiLibraryPackage).Assembly
            });
            container.Verify();

            return container;
        }
    }
}