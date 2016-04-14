using System.Collections.Generic;
using System.Linq;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Queries;

namespace GitHubExtension.Activity.Internal.WebApi.Extensions
{
    public static class ActivityContexQueryExtension
    {
        public static ICollection<ActivityEvent> GetCurrentRepositoryUserActivities(this IContextActivityQuery contextActivityQuery, int currentRepositoryId)
        {
            ICollection<ActivityEvent> activitesForCurrentRepo = contextActivityQuery.Activities.Where(r => r.CurrentRepositoryId == currentRepositoryId).ToList();

            return activitesForCurrentRepo;
        }

        public static ICollection<ActivityEvent> GetUserActivities(this IContextActivityQuery contextActivityQuery, string userId)
        {
            ICollection<ActivityEvent> userActivities = contextActivityQuery.Activities.Where(r => r.UserId == userId).ToList();

            return userActivities;
        }
    }
}
