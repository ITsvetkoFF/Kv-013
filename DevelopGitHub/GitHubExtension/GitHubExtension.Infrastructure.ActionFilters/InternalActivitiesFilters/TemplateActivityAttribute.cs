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
    public class TemplateActivityAttribute : ActionFilterAttribute, IInternalActivityFilter
    {
        public void SaveTemplateActivity(HttpActionExecutedContext actionExecutedContext, string activityTypeName)
        {
            var dependencyResolver = actionExecutedContext.GetDependencyResolver();
            var activityContextQuery = dependencyResolver.GetService<IActivityContextQuery>();
            var activityContextCommand = dependencyResolver.GetService<IActivityContextCommand>();

            var user = actionExecutedContext.GetUserModel();
            var repository = actionExecutedContext.GetRepositoryModel();

            AddTemplateActivity(activityContextQuery, activityContextCommand, user, repository, activityTypeName);
        }

        private void AddTemplateActivity(IActivityContextQuery activityContextQuery,
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
