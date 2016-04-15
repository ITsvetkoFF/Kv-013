using System.Linq;
using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Queries
{
    public class GetActivityTypeQuery : IGetActivityTypeQuery
    {
        private readonly ActivityContext _activityContext;

        public GetActivityTypeQuery(ActivityContext activityContext)
        {
            _activityContext = activityContext;
        }

        public IOrderedQueryable<ActivityType> ActivitiesTypes
        {
            get { return _activityContext.ActivitiesTypes.AsNoTracking(); }
        }

    }
}
