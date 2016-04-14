using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Queries
{
    public interface IGetActivityTypeQuery
    {
        ActivityType GetUserActivityType(string name);
    }
}
