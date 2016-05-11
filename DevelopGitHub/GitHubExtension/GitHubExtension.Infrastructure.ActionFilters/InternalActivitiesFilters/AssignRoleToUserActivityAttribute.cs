using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AssignRoleToUserActivityAttribute : InternalActivityFilter
    {
        public AssignRoleToUserActivityAttribute(
                                                IActivityContextQuery activityContextQuery, 
                                                IActivityContextCommand activityContextCommand, 
                                                ApplicationUserManager applicationUserManager)
            : base(activityContextQuery, activityContextCommand, applicationUserManager)
        {
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            int repoId = (int)actionExecutedContext.ActionContext.ActionArguments["repoId"];
            string roleToAssign = (string)actionExecutedContext.ActionContext.ActionArguments["roleToAssign"];
            int gitHubId = (int)actionExecutedContext.ActionContext.ActionArguments["gitHubId"];

            var userId = actionExecutedContext.ActionContext.RequestContext.Principal.Identity.GetUserId();
            var userName = actionExecutedContext.ActionContext.RequestContext.Principal.Identity.Name;

            User appUser = _applicationUserManager.FindByGitHubId(gitHubId);

            string collaboratorName = appUser.UserName;

            var activityType = _activityContextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            _activityContextCommand.AddActivity(new ActivityEvent()
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
