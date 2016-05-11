using System.Web.Http.Filters;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Security.DAL.Infrastructure;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    public abstract class InternalActivityFilter : ActionFilterAttribute
    {
        protected readonly IActivityContextQuery _activityContextQuery;
        protected readonly IActivityContextCommand _activityContextCommand;
        protected readonly ApplicationUserManager _applicationUserManager;

        protected InternalActivityFilter(
                                      IActivityContextQuery activityContextQuery,
                                      IActivityContextCommand activityContextCommand,
                                      ApplicationUserManager applicationUserManager)
        {
            _activityContextQuery = activityContextQuery;
            _activityContextCommand = activityContextCommand;
            _applicationUserManager = applicationUserManager;
        }
    }
}
