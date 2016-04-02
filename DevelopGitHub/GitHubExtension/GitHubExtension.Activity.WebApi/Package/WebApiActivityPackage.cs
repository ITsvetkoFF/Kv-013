using GitHubExtension.Activity.WebApi.Services.Implementation;
using GitHubExtension.Activity.WebApi.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Activity.WebApi.Package
{
    public class WebApiActivityPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IActivityWriterService, ActivityWriterService>(Lifestyle.Singleton);
        }
    }
}