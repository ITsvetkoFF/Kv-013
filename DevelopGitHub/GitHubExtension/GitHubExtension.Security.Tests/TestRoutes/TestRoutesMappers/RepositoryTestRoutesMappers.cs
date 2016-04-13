using GitHubExtension.Constant;
using System.Text.RegularExpressions;

namespace GitHubExtension.Security.Tests.TestRoutes.TestRoutesMappers
{
    public static class RepositoryTestRoutesMappers
    {
        public static string ForRepositoryGetById(this string url)
        {
            return url + Regex.Replace(
                RouteConstants.GetByIdRepository,
                RouteConstants.Id_int,
                "13");
        }

        public static string ForRepositoryGetReposForCurrentUser(this string url)
        {
            return url + RouteConstants.GetRepositoryForCurrentUser;
        }

        public static string ForRepositoryGetCollaboratorsForRepo(this string url)
        {
            return url + Regex.Replace(
                RouteConstants.GetCollaboratorsForRepository,
                RouteConstants.RepositoryName,
                "myRepository");
        }
    }
}
