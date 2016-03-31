using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Activity.WebApi;
using GitHubExtension.Activity.WebApi.Services.Interfaces;

namespace GitHubExtension.Activity.WebApi.Services.Implementation
{
    public class ActivityService : IActivityService
    {
        public bool AddActivity(DAL.ActivityType activityType, DateTime invokeTime, int userId, int currentProjectId)
        {
            throw new NotImplementedException();
        }
    }
}
