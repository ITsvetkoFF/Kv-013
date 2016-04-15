using System.Collections.Generic;
using System.Linq;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Queries;

namespace GitHubExtension.Activity.Internal.WebApi.Extensions
{
    public static class ActivityContexQueryExtension
    {
        public static IEnumerable<ActivityEvent> GetCurrentRepositoryUserActivities(this IContextActivityQuery contextActivityQuery, int currentRepositoryId)
        {
            IEnumerable<ActivityEvent> activitesForCurrentRepo = contextActivityQuery.Activities.Where(r => r.CurrentRepositoryId == currentRepositoryId);

            return activitesForCurrentRepo;
        }

        public static IEnumerable<ActivityEvent> GetUserActivities(this IContextActivityQuery contextActivityQuery, string userId)
        {
            IEnumerable<ActivityEvent> userActivities = contextActivityQuery.Activities.Where(r => r.UserId == userId);

            return userActivities;
        }

        public static ActivityType GetUserActivityType(this IContextActivityQuery contextActivityQuery, string name)
        {
            ActivityType userActivityType = contextActivityQuery.ActivitiesTypes.FirstOrDefault(n => n.Name == name);

            return userActivityType;
        }
    }
}
