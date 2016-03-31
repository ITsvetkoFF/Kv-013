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
        private ActivityModel activityModelContext;

        public ActivityService()
        {
            activityModelContext = new ActivityModel();
        }

        public bool AddActivity(ActivityType activityType, DateTime invokeTime, string userId, int currentProjectId)
        {
            try
            {
                ActivityEvent activity = new ActivityEvent() 
                { 
                  UserId = userId, 
                  CurrentProjectId = currentProjectId, 
                  ActivityType = activityType, 
                  InvokeTime = invokeTime
                };

                activityModelContext.Activities.Add(activity);

                activityModelContext.SaveChanges();

                return true;
            }
            catch (Exception) // Create custom exception
            {

                return false;
            }
        }

        public bool AddActivity(ActivityType activityType, DateTime invokeTime, string userId)
        {
            try
            {
                ActivityEvent activity = new ActivityEvent()
                {
                    UserId = userId,
                    ActivityType = activityType,
                    InvokeTime = invokeTime
                };

                activityModelContext.Activities.Add(activity);

                activityModelContext.SaveChanges();

                return true;
            }
            catch (Exception) // Create custom exception
            {
                return false;
            }
        }
    }
}
