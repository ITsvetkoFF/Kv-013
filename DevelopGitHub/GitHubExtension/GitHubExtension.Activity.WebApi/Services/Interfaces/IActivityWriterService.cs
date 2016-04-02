using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.WebApi.Services.Interfaces
{
    public interface IActivityWriterService
    {
        void AddActivity(ActivityEvent activityEvent);
        ActivityType GetActivityTypeByName(string name);
    }
}
 