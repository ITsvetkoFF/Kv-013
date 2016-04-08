using GitHubExtension.Activity.Internal.DAL;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using System.Collections.Generic;
using System.Linq;

namespace GitHubExtension.Activity.Internal.WebApi.Services
{
    public static class ActivityContextActivityQueryExtension
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
