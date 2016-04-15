using System.Linq;
using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Queries
{
    public interface IContextActivityQuery
    {
        IOrderedQueryable<ActivityEvent> Activities { get; }
       
        IOrderedQueryable<ActivityType> ActivitiesTypes { get; }
    }
}
