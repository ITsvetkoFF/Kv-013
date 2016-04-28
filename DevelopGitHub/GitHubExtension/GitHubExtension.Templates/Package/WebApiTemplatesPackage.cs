using GitHubExtension.Templates.Commands;
using GitHubExtension.Templates.DAL.Model;
using GitHubExtension.Templates.Queries;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Templates.Package
{
    public class WebApiTemplatesPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ITemplatesQuery, TemplatesQuery>(Lifestyle.Scoped);
            container.Register<ITemplatesCommand, TemplatesCommand>(Lifestyle.Scoped);
            container.Register<TemplatesContext>(Lifestyle.Singleton);
        }
    }
}