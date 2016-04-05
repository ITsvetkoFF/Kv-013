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
        public static ActivityEventViewModel ToActivityEventViewModel(this ActivityEvent activityEvent)
        {
            return new ActivityEventViewModel()
            {
                UserId = activityEvent.UserId,
                CurrentRepositoryId = activityEvent.CurrentRepositoryId,
                ActivityTypeId = activityEvent.ActivityTypeId,
                InvokeTime = activityEvent.InvokeTime,
                Message = activityEvent.Message
            };
        }
    }
}
