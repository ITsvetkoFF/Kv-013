using GitHubExtension.Infrastructure.Constants;

namespace GitHubExtension.Security.Test.TestRoutes.TestRoutesMappers
{
    public static class UserTestRoutesMappers
    {
        public static string ForGetAllUsersByName(this string url)
        {
            return url + RouteConstants.SearchUsersByName + "?username=test";
        }
    }
}
