using System.Linq;
using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Queries
{
    public interface IGetActivityTypeQuery
    {
        IOrderedQueryable<ActivityType> ActivitiesTypes { get; }
    }
}
