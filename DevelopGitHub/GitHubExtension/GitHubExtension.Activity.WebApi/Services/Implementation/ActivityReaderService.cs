using System;
using GitHubExtension.Activity.Internal.DAL;
using GitHubExtension.Activity.Internal.WebApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Activity.Internal.WebApi.Services.Implementation
{
    public class ActivityReaderService : IActivityReaderService
    {
        private ActivityModel activityModelContext;

        public ActivityReaderService()
        {
            activityModelContext = new ActivityModel();
        }

        public ICollection<ActivityEvent> GetCurrentRepositoryUserActivities(int currentRepositoryId)
        {
            ICollection<ActivityEvent> activitesForCurrentRepo = activityModelContext.Activities.Where(r => r.CurrentRepositoryId == currentRepositoryId).ToList();

            return activitesForCurrentRepo;
        }

        public ICollection<ActivityEvent> GetUserActivities(string userId)
        {
            ICollection<ActivityEvent> userActivities = activityModelContext.Activities.Where(r => r.UserId == userId).ToList();

            return userActivities;
        }
    }
}
