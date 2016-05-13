using GitHubExtension.Activity.External.WebAPI;

namespace GitHubExtension.Activity.External.Tests.TestsForRoutes.Extensions
{
    public static class ExternalActivityRouteTestExtensions
    {
        public static string ForGetGitHubActivityRoute(this string url, int page = 1)
        {
            return "/" + ExternalActivityRoutes.GetGitHubActivityRoute + "?page=" + page;
        }
    }
}
