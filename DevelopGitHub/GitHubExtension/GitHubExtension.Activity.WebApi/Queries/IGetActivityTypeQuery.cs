using GitHubExtension.Activity.Internal.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Queries
{
    public interface IGetActivityTypeQuery
    {
        ActivityType GetUserActivityType(string name);
    }
}
