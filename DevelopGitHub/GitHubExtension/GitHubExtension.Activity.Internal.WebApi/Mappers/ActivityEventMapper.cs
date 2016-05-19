using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Models;

namespace GitHubExtension.Activity.Internal.WebApi.Mappers
{
    public static class ActivityEventMapper
    {
        public static ActivityEvent ToActivityEventEntity(this ActivityEventModel activityEventModel)
        {
            return new ActivityEvent()
                       {
                           UserId = activityEventModel.UserId, 
                           CurrentRepositoryId = activityEventModel.CurrentRepositoryId, 
                           ActivityTypeId = activityEventModel.ActivityTypeId, 
                           InvokeTime = activityEventModel.InvokeTime,
                           ImageUrl = activityEventModel.ImageUrl,
                           Message = activityEventModel.Message
                       };
        }

        public static ActivityEventModel ToActivityEventModel(this ActivityEvent activityEvent)
        {
            return new ActivityEventModel()
                       {
                           UserId = activityEvent.UserId, 
                           CurrentRepositoryId = activityEvent.CurrentRepositoryId, 
                           ActivityTypeId = activityEvent.ActivityTypeId, 
                           InvokeTime = activityEvent.InvokeTime,
                           ImageUrl = activityEvent.ImageUrl,
                           Message = activityEvent.Message
                       };
        }
    }
}