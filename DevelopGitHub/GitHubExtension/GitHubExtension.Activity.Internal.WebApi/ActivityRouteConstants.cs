namespace GitHubExtension.Activity.Internal.WebApi
{
    public class ActivityRouteConstants
    {
        public const string AddJoinToSystemActivity = "api/activity/internal/joinToSystem";

        public const string AddRoleActivityForCurrentRepository = "api/activity/internal/addRole";

        public const string CurrentRepositoryActivityRoute = "api/activity/internal";

        public const string CurrentUserActivityRoute = "api/activity/internal/{userId:guid}";

        public const string RepositoryAddedToSystemActivity = "api/activity/internal/repositoryAddedToSystem";
    }
}