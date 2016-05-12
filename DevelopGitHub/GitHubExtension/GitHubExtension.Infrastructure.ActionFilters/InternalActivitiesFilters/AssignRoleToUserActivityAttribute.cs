using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Queries;
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
            int gitHubId = actionExecutedContext.GetGitHubId();
            string userId = actionExecutedContext.GetUserId();
            string userName = actionExecutedContext.GetUserName();
            var dependencyResolver = actionExecutedContext.GetDependencyResolver();
            var collaboratorName = dependencyResolver.GetCollaboratorName(gitHubId);
            var activityContextQuery = dependencyResolver.GetService<ActivityContextQuery>();
            var activityContextCommand = dependencyResolver.GetService<ActivityContextCommand>();
            var activityType = activityContextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            if (activityContextCommand != null)
                activityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = userId,
                    ActivityTypeId = activityType.Id,
                    CurrentRepositoryId = repoId,
                    InvokeTime = DateTime.Now,
                    Message = string.Format(
                        "{0} {1} {2} to {3}",
                        userName,
                        activityType.Name,
                        roleToAssign,
                        collaboratorName)
                });
        }
    }
}
