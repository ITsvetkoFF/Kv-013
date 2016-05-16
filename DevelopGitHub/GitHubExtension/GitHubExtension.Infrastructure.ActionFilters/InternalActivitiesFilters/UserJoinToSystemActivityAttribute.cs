using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Infrastructure.ActionFilters.Models;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UserJoinToSystemActivityAttribute : InternalActivityFilter
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            SeedCommonMembers(actionExecutedContext);
            AddRoleActivity(_activityContextQuery, _activityContextCommand, _user);
        }

        private void AddRoleActivity(IActivityContextQuery activityContextQuery,
                                    IActivityContextCommand activityContextCommand,
                                    UserModel user)
        {
            var activityType = activityContextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            string message = CreateActivityMessage(user.UserName, activityType.Name);

            if (activityContextCommand != null)
                activityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = user.UserId,
                    ActivityTypeId = activityType.Id,
                    InvokeTime = DateTime.Now,
                    Message = message
                });
        }
    }
}
