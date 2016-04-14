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

        public ActivityType GetUserActivityType(string name)
        {
            return _activityContext.ActivitiesTypes.FirstOrDefault(n => n.Name == name);
        }
    }
}
