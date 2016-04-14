using System.Linq;
using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Queries
{
    public class ContextActivityQuery : IContextActivityQuery
    {
        private readonly ActivityContext _activityContext;

        public ContextActivityQuery(ActivityContext activityContext)
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
    }
}
