using SimpleInjector;
using SimpleInjector.Packaging;
using GitHubExtension.Activity.Internal.DAL;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Activity.Internal.WebApi.Commands;

namespace GitHubExtension.Activity.Internal.WebApi.Package
{
    public class WebApiActivityPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ActivityContext>(Lifestyle.Scoped);
            container.Register<IContextActivityQuery, ContextActivityQuery>(Lifestyle.Scoped);
            container.Register<IContextActivityCommand, ContextActivityCommand>(Lifestyle.Scoped);
            container.Register<IGetActivityTypeQuery, GetActivityTypeQuery>(Lifestyle.Scoped);
        }
    }
}