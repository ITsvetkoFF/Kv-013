using GitHubExtension.Activity.Internal.DAL;
using System.Linq;


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
