using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.GitHubExtension.Activity.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Activity.GitHubExtension.Activity.WebApi.Mappers
{
    public static class ActivityEventMapper
    {
        public static ActivityEvent ToEntity(this ActivityEventDto activityEvent)
        {
            var activityEventEntity = new ActivityEvent()
            {
                Id = activityEvent.Id,
                UserId = activityEvent.UserId,
                CurrentProjectId = activityEvent.CurrentProjectId,
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
                CurrentProjectId = activityEvent.CurrentProjectId,
                ActivityTypeId = activityEvent.ActivityTypeId,
                InvokeTime = activityEvent.InvokeTime
            };
        }
    }
}
