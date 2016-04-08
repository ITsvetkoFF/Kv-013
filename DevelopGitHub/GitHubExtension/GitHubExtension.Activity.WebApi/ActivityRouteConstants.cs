

namespace GitHubExtension.Activity.Internal.WebApi
{
    public class ActivityRouteConstants
    {
        public const string CurrentRepositoryActivityRoute = "api/activity/internal/{currentRepositoryId}";
        public const string CurrentUserActivityRoute = "api/activity/internal/{userId:guid}";
    }
}
