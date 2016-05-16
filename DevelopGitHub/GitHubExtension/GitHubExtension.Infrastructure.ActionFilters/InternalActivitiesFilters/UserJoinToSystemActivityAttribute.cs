using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Infrastructure.ActionFilters.Extensions;
using GitHubExtension.Infrastructure.ActionFilters.Models;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UserJoinToSystemActivityAttribute : ActionFilterAttribute, IInternalActivityFilter
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var activityContextQuery = actionExecutedContext.GetIActivityContextQuery();
            var activityContextCommand = actionExecutedContext.GetIActivityContextCommand();
            var user = actionExecutedContext.GetUserModel();

            AddRoleActivity(activityContextQuery, activityContextCommand, user);
        }

        private void AddRoleActivity(IActivityContextQuery activityContextQuery,
                                    IActivityContextCommand activityContextCommand,
                                    UserModel user)
        {
            var activityType = activityContextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            if (activityContextCommand != null)
                activityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = user.UserId,
                    ActivityTypeId = activityType.Id,
                    InvokeTime = DateTime.Now,
                    Message = string.Format(
                        "{0} {1}",
                        user.UserName,
                        activityType.Name)
                });
        }
    }
}
