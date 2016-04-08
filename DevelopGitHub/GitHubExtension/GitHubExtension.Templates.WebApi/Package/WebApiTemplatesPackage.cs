using GitHubExtension.Templates.WebApi.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Templates.WebApi.Package
{
    public class WebApiTemplatesPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ITemplateService, TemplatesService>(Lifestyle.Singleton);
        }
    }
}