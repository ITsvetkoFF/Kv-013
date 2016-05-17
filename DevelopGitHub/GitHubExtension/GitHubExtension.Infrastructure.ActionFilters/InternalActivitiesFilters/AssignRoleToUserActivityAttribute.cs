using System;
using System.Web.Http.Filters;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Infrastructure.ActionFilters.Extensions;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AssignRoleToUserActivityAttribute : InternalActivityFilter
    {
        private string _roleToAssign;
        private string _collaboratorName;
        private int _repoId;

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _roleToAssign = actionExecutedContext.GetRoleToAssign();
            _collaboratorName = actionExecutedContext.GetUserByGitHubId().UserName;
            _repoId = actionExecutedContext.GetRepositoryId();

            base.OnActionExecuted(actionExecutedContext);
        }

        public override string ActivityTypeName
        {
            get { return ActivityTypeNames.AddRole; }
        }

        protected override string BuildActivityMessage()
        {
            return string.Format("{0} to {1}", _roleToAssign, _collaboratorName);
        }

        protected override ActivityEvent BuildPartOfActivityEvent()
        {
            var activityEvent = base.BuildPartOfActivityEvent();

            activityEvent.CurrentRepositoryId = _repoId;

            return activityEvent;
        }
    }
}
