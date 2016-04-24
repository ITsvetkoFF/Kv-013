using GitHubExtension.Templates.Services;

using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Templates.Package
{
    public class WebApiTemplatesPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ITemplateService, TemplatesService>(Lifestyle.Singleton);
        }
    }
}