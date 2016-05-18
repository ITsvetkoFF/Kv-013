using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Infrastructure.ActionFilters.Extensions;
using GitHubExtension.Infrastructure.ActionFilters.Models;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TemplateActivityAttribute : InternalActivityFilter
    {
        private RepositoryModel _repository;

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _repository = actionExecutedContext.GetRepositoryModel();

            base.OnActionExecuted(actionExecutedContext);
        }

        protected override string BuildActivityMessage()
        {
            return string.Format("for {0}", _repository.Name);
        }

        protected override ActivityEvent BuildPartOfActivityEvent()
        {
            var activityEvent = base.BuildPartOfActivityEvent();

            activityEvent.CurrentRepositoryId = _repository.Id;

            return activityEvent;
        }
    }
}
