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

            SaveActivityEvent(ActivityContextQuery, ActivityContextCommand, User, repository, ActivityTypeName);
        }

        private void SaveActivityEvent(
                                        IActivityContextQuery activityContextQuery,
                                        IActivityContextCommand activityContextCommand,
                                        UserModel user,
                                        RepositoryModel repository,
                                        string activityTypeName)
        {
            var activityType = activityContextQuery.GetUserActivityType(activityTypeName);

            string message = CreateActivityMessage(user.UserName, activityType.Name, repository.Name);

            if (activityContextCommand != null)
            {
                activityContextCommand.AddActivity(new ActivityEvent()
                {
                    UserId = user.UserId,
                    ActivityTypeId = activityType.Id,
                    CurrentRepositoryId = repository.Id,
                    InvokeTime = DateTime.Now,
                    Message = message
                });
            }         
        }
    }
}
