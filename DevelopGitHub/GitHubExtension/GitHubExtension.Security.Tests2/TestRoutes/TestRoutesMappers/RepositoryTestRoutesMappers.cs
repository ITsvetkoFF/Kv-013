using System.Text.RegularExpressions;
using GitHubExtension.Infrastructure.Constants;

namespace GitHubExtension.Security.Tests2.TestRoutes.TestRoutesMappers
{
    public static class RepositoryTestRoutesMappers
    {
        public static string ForRepositoryGetById(this string url)
        {
            return url + Regex.Replace(RouteConstants.GetByIdRepository, RouteConstants.IdInt, "/13");
        }

        public static string ForRepositoryGetCollaboratorsForRepo(this string url)
        {
            return url
                   + Regex.Replace(
                       RouteConstants.GetCollaboratorsForRepository, 
                       RouteConstants.RepositoryName, 
                       "/myRepository");
        }

        public static string ForRepositoryGetReposForCurrentUser(this string url)
        {
            return url + RouteConstants.GetRepositoryForCurrentUser;
        }
    }
}