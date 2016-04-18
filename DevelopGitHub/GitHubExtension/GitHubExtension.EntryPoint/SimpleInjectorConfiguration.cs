using GitHubExtension.Activity.External.WebAPI.Package;
using GitHubExtension.Notes.WebApi.Package;
using GitHubExtension.Activity.Internal.WebApi.Package;
using GitHubExtension.Security.WebApi.Package;
using GitHubExtension.Templates.Package;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace GitHubExtension.EntryPoint
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
                typeof(ExternalActivityPackage).Assembly,
                typeof(SecurityPackage).Assembly,
                typeof(NotesPackage).Assembly,
                typeof(ActivityPackage).Assembly,
                typeof(WebApiTemplatesPackage).Assembly
            });
            container.Verify();

            return container;
        }
    }
}