using System.Web.Http.Filters;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Infrastructure.ActionFilters.Extensions;
using GitHubExtension.Infrastructure.ActionFilters.Models;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    public class InternalActivityFilter : ActionFilterAttribute
    {
        public IActivityContextQuery ActivityContextQuery { get; set; }

        public IActivityContextCommand ActivityContextCommand { get; set; }

        public UserModel User { get; set; }

        protected void SeedCommonMembers(HttpActionExecutedContext actionExecutedContext)
        {
            ActivityContextQuery = actionExecutedContext.GetIActivityContextQuery();
            ActivityContextCommand = actionExecutedContext.GetIActivityContextCommand();
            User = actionExecutedContext.GetUserModel();
        }

        protected virtual string CreateActivityMessage(string userName, string activityTypeName, string repositoryName)
        {
            string message = string.Format(
                "{0} {1} for {2}",
                userName,
                activityTypeName,
                repositoryName);

            return message;
        }

        protected virtual string CreateActivityMessage(string userName, string activityTypeName)
        {
            string message = string.Format(
                                    "{0} {1}",
                                    userName,
                                    activityTypeName);

            return message;
        }

        protected virtual string CreateActivityMessage(
                                                      string userName,
                                                      string activityTypeName,
                                                      string roleToAssign,
                                                      string collaboratorName)
        {
            string messsage = string.Format(
                "{0} {1} {2} to {3}",
                userName,
                activityTypeName,
                roleToAssign,
                collaboratorName);

            return messsage;
        }
    }
}
