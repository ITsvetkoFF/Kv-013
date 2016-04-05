using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Activity.Internal.WebApi;
using GitHubExtension.Activity.Internal.WebApi.Services.Interfaces;
using GitHubExtension.Activity.Internal.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Services.Implementation
{
    public class ActivityWriterService : IActivityWriterService
    {
        private ActivityModel activityModelContext;

        public ActivityWriterService()
        {
            activityModelContext = new ActivityModel();
        }

        public void AddActivity(ActivityEvent activityEvent) 
        {
            activityModelContext.Activities.Add(activityEvent);

            activityModelContext.SaveChanges();
        }

        public ActivityType GetActivityTypeByName(string name)
        {
            ActivityType activityType = activityModelContext.ActivitiesTypes.FirstOrDefault(n => n.Name == name);

            return activityType;
        }
    }
}
 