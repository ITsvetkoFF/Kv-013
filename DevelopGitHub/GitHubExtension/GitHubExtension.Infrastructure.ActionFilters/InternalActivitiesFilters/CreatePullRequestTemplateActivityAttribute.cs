using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CreatePullRequestTemplateActivityAttribute : TemplateActivityAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
             SaveTemplateActivity(actionExecutedContext, ActivityTypeNames.CreatePullRequestTemplate);
        }
    }
}
