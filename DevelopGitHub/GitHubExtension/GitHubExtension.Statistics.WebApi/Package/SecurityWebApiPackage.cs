using GitHubExtension.Statistics.WebApi.Services.Implementations;
using GitHubExtension.Statistics.WebApi.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Statistics.WebApi.Package
{
    public class SecurityWebApiPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IGitHubService, GitHubService>(Lifestyle.Singleton);
            container.Register<IStatisticsService, StatisticsService>(Lifestyle.Singleton);
        }
    }
}
