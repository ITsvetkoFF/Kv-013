using GitHubExtension.Notes.DAL.Package;
using GitHubExtension.Notes.WebApi.Package;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
namespace GitHubExtension.Notes.WebApi
{
    public class SimpleInjectorConfiguration
    {
        public static Container ConfigurationSimpleInjector(Container container)
        {
            // Container container = new Container();
            // container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            return RegisterPackages(container);
        }

        public static Container RegisterPackages(Container container)
        {
            container.RegisterPackages(new[]
            {
                typeof(NotesDALPackage).Assembly,
                typeof(NotesWebApiPackage).Assembly
            });
            

            return container;
        } 
    }
}