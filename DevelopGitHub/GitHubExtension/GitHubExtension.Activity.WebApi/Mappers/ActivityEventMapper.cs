using GitHubExtension.Activity.Internal.DAL;
using GitHubExtension.Activity.Internal.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Activity.Internal.WebApi.Mappers
{
    public static class ActivityEventMapper
    {
        public static ActivityEvent ToEntity(this ActivityEventDto activityEvent)
        {
            var activityEventEntity = new ActivityEvent()
            {
                Id = activityEvent.Id,
                UserId = activityEvent.UserId,
                CurrentRepositoryId = activityEvent.CurrentRepositoryId,
                ActivityTypeId = activityEvent.ActivityTypeId,
                InvokeTime = activityEvent.InvokeTime
            };

            return activityEventEntity;
        }

        public static ActivityEventViewModel ToActivityEventViewModel(this ActivityEvent activityEvent)
        {
            return new ActivityEventViewModel()
            {
                Id = activityEvent.Id,
                UserId = activityEvent.UserId,
                CurrentRepositoryId = activityEvent.CurrentRepositoryId,
                ActivityTypeId = activityEvent.ActivityTypeId,
                InvokeTime = activityEvent.InvokeTime
            };
        }
    }
}
