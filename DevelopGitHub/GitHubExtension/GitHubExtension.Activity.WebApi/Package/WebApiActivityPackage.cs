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
            container.Register<IContextActivityQuery, ContextActivityQuery>(Lifestyle.Singleton);
            container.Register<IContextActivityCommand, ContextActivityCommand>(Lifestyle.Singleton);
            container.Register<IGetActivityTypeQuery, GetActivityTypeQuery>(Lifestyle.Singleton);
            container.Register<ActivityContext>(Lifestyle.Scoped);
        }
    }
}