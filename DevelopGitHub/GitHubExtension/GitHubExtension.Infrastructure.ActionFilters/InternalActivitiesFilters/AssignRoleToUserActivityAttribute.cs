using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Infrastructure.ActionFilters.Extensions;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AssignRoleToUserActivityAttribute : InternalActivityFilter
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            int repoId = actionExecutedContext.GetRepositoryId();
            string roleToAssign = actionExecutedContext.GetRoleToAssign();
            string collaboratorName = actionExecutedContext.GetUserByGitHubId().UserName;

            SeedCommonMembers(actionExecutedContext);

            AddRoleActivity(repoId, roleToAssign, collaboratorName); 
        }

        private void AddRoleActivity(int repoId, string roleToAssign, string collaboratorName)
        {
            var activityType = ActivityContextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            string message = CreateActivityMessage(User.UserName, activityType.Name, roleToAssign, collaboratorName);

            if (ActivityContextCommand != null)
            {
                ActivityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = User.UserId,
                    ActivityTypeId = activityType.Id,
                    CurrentRepositoryId = repoId,
                    InvokeTime = DateTime.Now,
                    Message = message
                });
            }         
        }

        private string CreateActivityMessage(
                                               string userName,
                                               string activityTypeName,
                                               string roleToAssign,
                                               string collaboratorName)
        {
            string baseMessage = base.CreateActivityMessage(userName, activityTypeName);

            string message = string.Format("{0} {1} to {2}", baseMessage, roleToAssign, collaboratorName);

            return message;
        }
    }
}
