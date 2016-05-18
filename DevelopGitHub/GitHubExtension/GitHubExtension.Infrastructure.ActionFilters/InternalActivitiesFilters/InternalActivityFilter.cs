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
    public abstract class InternalActivityFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            ActivityContextQuery = actionExecutedContext.GetIActivityContextQuery();
            ActivityContextCommand = actionExecutedContext.GetIActivityContextCommand();
            User = actionExecutedContext.GetUserModel();

            AddActivity();
        }

        public virtual string ActivityTypeName { get; set; }

        private IActivityContextQuery ActivityContextQuery { get; set; }

        private IActivityContextCommand ActivityContextCommand { get; set; }

        private UserModel User { get; set; }

        protected virtual string BuildActivityMessage()
        {
            return string.Empty;
        }

        protected virtual ActivityEvent BuildPartOfActivityEvent()
        {
            var activityType = ActivityContextQuery.GetUserActivityType(ActivityTypeName);

            return new ActivityEvent
            {
                UserId = User.UserId,
                ActivityTypeId = activityType.Id,
                InvokeTime = DateTime.Now,
                Message = string.Format("{0} {1} {2}", User.UserName, ActivityTypeName, BuildActivityMessage())
            };
        }

        private void AddActivity()
        {
            if (ActivityContextCommand != null)
            {
                var activityEvent = BuildPartOfActivityEvent();
                ActivityContextCommand.AddActivity(activityEvent);
            }
        }
    }
}
