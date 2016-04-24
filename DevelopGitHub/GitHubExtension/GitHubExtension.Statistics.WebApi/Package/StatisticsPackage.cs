using GitHubExtension.Statistics.WebApi.Queries.Implementations;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;

using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Statistics.WebApi.Package
{
    public class StatisticsPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IStatisticsQuery, StatisticsQuery>(Lifestyle.Transient);
            container.Register<IGitHubQuery, GitHubQuery>(Lifestyle.Transient);
        }
    }
}