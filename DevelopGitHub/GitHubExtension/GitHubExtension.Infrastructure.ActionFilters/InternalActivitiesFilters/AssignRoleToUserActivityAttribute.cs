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
    public class AssignRoleToUserActivityAttribute : InternalActivityFilter
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            int repoId = actionExecutedContext.GetRepositoryId();
            string roleToAssign = actionExecutedContext.GetRoleToAssign();
            string collaboratorName = actionExecutedContext.GetUserByGitHubId().UserName;

            SeedCommonMembers(actionExecutedContext);

            AddRoleActivity(ActivityContextQuery, ActivityContextCommand, User, repoId, roleToAssign, collaboratorName); 
        }

        private void AddRoleActivity(
                                    IActivityContextQuery activityContextQuery, 
                                    IActivityContextCommand activityContextCommand, 
                                    UserModel user, 
                                    int repoId, 
                                    string roleToAssign,
                                    string collaboratorName)
        {
            var activityType = activityContextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            string message = CreateActivityMessage(user.UserName, activityType.Name, roleToAssign, collaboratorName);

            if (activityContextCommand != null)
            {
                activityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = user.UserId,
                    ActivityTypeId = activityType.Id,
                    CurrentRepositoryId = repoId,
                    InvokeTime = DateTime.Now,
                    Message = message
                });
            }         
        }
    }
}
