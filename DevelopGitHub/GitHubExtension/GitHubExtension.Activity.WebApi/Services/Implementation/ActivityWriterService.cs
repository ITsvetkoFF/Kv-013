using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Activity.WebApi;
using GitHubExtension.Activity.WebApi.Services.Interfaces;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.WebApi.Exceptions;

namespace GitHubExtension.Activity.WebApi.Services.Implementation
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
 