using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Activity.WebApi;
using GitHubExtension.Activity.WebApi.Services.Interfaces;
using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.WebApi.Services.Implementation
{
    public class ActivityService : IActivityService
    {

        public bool AddActivity(ActivityType activityType, DateTime invokeTime, string userId, int currentProjectId)
        {
            throw new NotImplementedException();
        }


        public bool AddActivity(ActivityType activityType, DateTime invokeTime, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
