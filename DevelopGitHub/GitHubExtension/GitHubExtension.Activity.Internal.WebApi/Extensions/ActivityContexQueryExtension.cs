﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Queries;

namespace GitHubExtension.Activity.Internal.WebApi.Extensions
{
    public static class ActivityContexQueryExtension
    {
        public static IEnumerable<ActivityEvent> GetCurrentRepositoryUserActivities(
            this IActivityContextQuery contextActivityQuery, 
            int currentRepositoryId)
        {
            IEnumerable<ActivityEvent> activitesForCurrentRepo =
                contextActivityQuery.Activities.Where(r => r.CurrentRepositoryId == currentRepositoryId).AsNoTracking();

            return activitesForCurrentRepo;
        }

        public static IEnumerable<ActivityEvent> GetUserActivities(
            this IActivityContextQuery contextActivityQuery, 
            string userId)
        {
            IEnumerable<ActivityEvent> userActivities =
                contextActivityQuery.Activities.Where(r => r.UserId == userId).AsNoTracking();

            return userActivities;
        }

        public static ActivityType GetUserActivityType(this IActivityContextQuery contextActivityQuery, string name)
        {
            ActivityType userActivityType =
                contextActivityQuery.ActivitiesTypes.AsNoTracking().FirstOrDefault(n => n.Name == name);

            return userActivityType;
        }
    }
}