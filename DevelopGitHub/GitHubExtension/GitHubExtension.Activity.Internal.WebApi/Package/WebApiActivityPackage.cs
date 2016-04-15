using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Activity.Internal.WebApi.Package
{
    public class WebApiActivityPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ActivityContext>(Lifestyle.Scoped);
            container.Register<IContextActivityQuery, ContextActivityQuery>(Lifestyle.Scoped);
            container.Register<IContextActivityCommand, ContextActivityCommand>(Lifestyle.Scoped);
        }
    }
}
