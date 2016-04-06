using GitHubExtension.Activity.External.WebAPI.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Activity.External.WebAPI.Package
{
    public class ExternalActivityPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IGitHubService, GitHubService>(Lifestyle.Scoped);
        }
    }
}
