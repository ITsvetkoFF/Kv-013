using GitHubExtension.Activity.Internal.WebApi.Services.Implementation;
using GitHubExtension.Activity.Internal.WebApi.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Activity.Internal.WebApi.Package
{
    public class WebApiActivityPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IActivityWriterService, ActivityWriterService>(Lifestyle.Singleton);
            container.Register<IActivityReaderService, ActivityReaderService>(Lifestyle.Singleton);
        }
    }
}