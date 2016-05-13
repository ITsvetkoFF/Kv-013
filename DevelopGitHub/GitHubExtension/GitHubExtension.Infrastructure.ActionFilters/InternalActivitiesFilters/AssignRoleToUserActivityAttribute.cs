using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Infrastructure.ActionFilters.Extensions;
using GitHubExtension.Infrastructure.ActionFilters.Models;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;

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
            var dependencyResolver = actionExecutedContext.GetDependencyResolver();
            var applicationUserManager = dependencyResolver.GetService<ApplicationUserManager>();
            User appUser = applicationUserManager.FindByGitHubId(gitHubId);
            string collaboratorName = appUser.UserName;
            var activityContextQuery = dependencyResolver.GetService<ActivityContextQuery>();
            var activityContextCommand = dependencyResolver.GetService<ActivityContextCommand>();
            var user = actionExecutedContext.GetUserModel();

            AddRoleActivity(activityContextQuery, activityContextCommand, user, repoId, roleToAssign, collaboratorName); 
        }

        public void AddRoleActivity(ActivityContextQuery activityContextQuery, 
                                    ActivityContextCommand activityContextCommand, 
                                    UserModel user, 
                                    int repoId, 
                                    string roleToAssign,
                                    string collaboratorName)
        {
            var activityType = activityContextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            if (activityContextCommand != null)
                activityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = user.UserId,
                    ActivityTypeId = activityType.Id,
                    CurrentRepositoryId = repoId,
                    InvokeTime = DateTime.Now,
                    Message = string.Format(
                        "{0} {1} {2} to {3}",
                        user.UserName,
                        activityType.Name,
                        roleToAssign,
                        collaboratorName)
                });
        }
    }
}
