using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Infrastructure.ActionFilters.Extensions;
using GitHubExtension.Infrastructure.ActionFilters.Models;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TemplateActivityAttribute : InternalActivityFilter
    {
        public string ActivityTypeName { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            SeedCommonMembers(actionExecutedContext);
            SaveTemplateActivity(actionExecutedContext);
        }

        private void SaveTemplateActivity(HttpActionExecutedContext actionExecutedContext)
        {
            var repository = actionExecutedContext.GetRepositoryModel();

            SaveActivityEvent(repository);
        }

        private void SaveActivityEvent(RepositoryModel repository)
        {
            var activityType = ActivityContextQuery.GetUserActivityType(ActivityTypeName);

            string message = CreateActivityMessage(User.UserName, activityType.Name, repository.Name);

            if (ActivityContextCommand != null)
            {
                ActivityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = User.UserId,
                    ActivityTypeId = activityType.Id,
                    CurrentRepositoryId = repository.Id,
                    InvokeTime = DateTime.Now,
                    Message = message
                });
            }         
        }

        private string CreateActivityMessage(string userName, string activityTypeName, string repositoryName)
        {
            string baseMessage = base.CreateActivityMessage(userName, activityTypeName);

            string message = string.Format("{0} for {1}", baseMessage, repositoryName);

            return message;
        }
    }
}
