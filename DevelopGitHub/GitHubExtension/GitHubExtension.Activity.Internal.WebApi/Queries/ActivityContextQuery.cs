using System.Linq;

using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Queries
{
    public class ActivityContextQuery : IActivityContextQuery
    {
        private readonly ActivityContext _activityContext;

        public ActivityContextQuery(ActivityContext activityContext)
        {
            _activityContext = activityContext;
        }

        public IOrderedQueryable<ActivityEvent> Activities
        {
            get
            {
                return _activityContext.Activities;
            }
        }

        public IOrderedQueryable<ActivityType> ActivitiesTypes
        {
            get
            {
                return _activityContext.ActivitiesTypes;
            }
        }
    }
}