using GitHubExtension.Activity.External.WebAPI.Queries;

using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Activity.External.WebAPI.Package
{
    public class ExternalActivityPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IGitHubEventsQuery, GitHubEventsQuery>(Lifestyle.Scoped);
        }
    }
}