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
    public class TemplateActivityAttribute : ActionFilterAttribute, IInternalActivityFilter
    {
        public string ActivityTypeName { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            SaveTemplateActivity(actionExecutedContext);
        }

        private void SaveTemplateActivity(HttpActionExecutedContext actionExecutedContext)
        {
            var activityContextQuery = actionExecutedContext.GetIActivityContextQuery();
            var activityContextCommand = actionExecutedContext.GetIActivityContextCommand();

            var user = actionExecutedContext.GetUserModel();
            var repository = actionExecutedContext.GetRepositoryModel();

            SaveActivityEvent(activityContextQuery, activityContextCommand, user, repository, ActivityTypeName);
        }

        private void SaveActivityEvent(IActivityContextQuery activityContextQuery,
                                IActivityContextCommand activityContextCommand,
                                UserModel user,
                                RepositoryModel repository,
                                string activityTypeName)
        {
            var activityType = activityContextQuery.GetUserActivityType(activityTypeName);

            if (activityContextCommand != null)
                activityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = user.UserId,
                    ActivityTypeId = activityType.Id,
                    CurrentRepositoryId = repository.Id,
                    InvokeTime = DateTime.Now,
                    Message = string.Format(
                        "{0} {1} for {2}",
                        user.UserName,
                        activityType.Name,
                        repository.Name)
                });
        }
    }
}
