using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Activity.Internal.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Services.Interfaces
{
    public interface IActivityWriterService
    {
        void AddActivity(ActivityEvent activityEvent);

        // Additional method for geting activity type by name
        ActivityType GetActivityTypeByName(string name);
    }
}
 