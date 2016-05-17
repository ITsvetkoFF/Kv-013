using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Extensions;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UserJoinToSystemActivityAttribute : InternalActivityFilter
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            SeedCommonMembers(actionExecutedContext);
            AddJoinToSystemActivity();
        }

        private void AddJoinToSystemActivity()
        {
            var activityType = ActivityContextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            string message = CreateActivityMessage(User.UserName, activityType.Name);

            if (ActivityContextCommand != null)
            {
                ActivityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = User.UserId,
                    ActivityTypeId = activityType.Id,
                    InvokeTime = DateTime.Now,
                    Message = message
                });
            }
        }
    }
}
