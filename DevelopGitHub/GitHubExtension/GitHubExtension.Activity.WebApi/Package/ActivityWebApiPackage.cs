using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Packaging;
using GitHubExtension.Activity.WebApi.Services.Interfaces;
using GitHubExtension.Activity.WebApi.Services.Implementation;

namespace GitHubExtension.Activity.WebApi.Package
{
    public class ActivityWebApiPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IActivityService, ActivityService>(Lifestyle.Singleton);
            container.Verify();
        }
    }
}
