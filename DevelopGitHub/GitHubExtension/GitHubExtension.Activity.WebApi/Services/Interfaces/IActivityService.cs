using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.WebApi.Services.Interfaces
{
    public interface IActivityService
    {
        bool AddActivity(ActivityType activityType, DateTime invokeTime, string userId, int currentProjectId);
        bool AddActivity(ActivityType activityType, DateTime invokeTime, string userId);
    }
}
