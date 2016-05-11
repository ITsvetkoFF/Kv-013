using System;
using System.Web;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AssignRoleToUserActivityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            int repoId = (int)actionExecutedContext.ActionContext.ActionArguments["repoId"];
            string roleToAssign = (string)actionExecutedContext.ActionContext.ActionArguments["roleToAssign"];
            int gitHubId = (int)actionExecutedContext.ActionContext.ActionArguments["gitHubId"];

            var userId = HttpContext.Current.User.Identity.GetUserId();
            var userName = HttpContext.Current.User.Identity.Name;

            var contextQuery = HttpContext.Current.GetOwinContext().Get<ActivityContextQuery>();

            var contextCommand = HttpContext.Current.GetOwinContext().Get<ActivityContextCommand>();

            var userManager = HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();

            User appUser = userManager.FindByGitHubId(gitHubId);

            string collaboratorName = appUser.UserName;

            var activityType = contextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            contextCommand.AddActivity(new ActivityEvent()
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
