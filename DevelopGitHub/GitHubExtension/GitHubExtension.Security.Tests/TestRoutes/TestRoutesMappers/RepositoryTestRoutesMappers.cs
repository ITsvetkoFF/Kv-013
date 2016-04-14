﻿using System.Text.RegularExpressions;
using GitHubExtension.Constant;

namespace GitHubExtension.Security.Tests.TestRoutes.TestRoutesMappers
{
    public static class RepositoryTestRoutesMappers
    {
        public static string ForRepositoryGetById(this string url)
        {
            return url + Regex.Replace(
                RouteConstants.GetByIdRepository,
                RouteConstants.IdInt,
                "/13");
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
                "/myRepository");
        }
    }
}
