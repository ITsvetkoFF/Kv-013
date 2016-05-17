using System.Web.Http.Filters;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Infrastructure.ActionFilters.Extensions;
using GitHubExtension.Infrastructure.ActionFilters.Models;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    public class InternalActivityFilter : ActionFilterAttribute
    {
        protected IActivityContextQuery ActivityContextQuery { get; set; }

        protected IActivityContextCommand ActivityContextCommand { get; set; }

        protected UserModel User { get; set; }

        protected void SeedCommonMembers(HttpActionExecutedContext actionExecutedContext)
        {
            ActivityContextQuery = actionExecutedContext.GetIActivityContextQuery();
            ActivityContextCommand = actionExecutedContext.GetIActivityContextCommand();
            User = actionExecutedContext.GetUserModel();
        }

        protected string CreateActivityMessage(string userName, string activityTypeName)
        {
            string message = string.Format(
                                    "{0} {1}",
                                    userName,
                                    activityTypeName);

            return message;
        }
    }
}
