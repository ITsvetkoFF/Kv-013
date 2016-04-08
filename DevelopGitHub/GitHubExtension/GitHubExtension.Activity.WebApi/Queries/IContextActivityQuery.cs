using GitHubExtension.Activity.Internal.DAL;
using System.Linq;

namespace GitHubExtension.Activity.Internal.WebApi.Queries
{
    public interface IContextActivityQuery
    {
        IOrderedQueryable<ActivityEvent> Activities { get; }
    }
}
