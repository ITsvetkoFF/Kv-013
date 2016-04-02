using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Statistics.WebApi.Package
{
    public class SecurityWebApiPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IGitHubService, GitHubService>(Lifestyle.Singleton);
        }
    }
}
