using GitHubExtension.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitHubExtension.Security.Tests.TestRoutes.TestRoutesMappers
{
    public static class AccountTestRoutesMappers
    {
        public static string ForAccountGetUser(this string url)
        {
            return url + Regex.Replace(
                RouteConstants.GetUser,
                RouteConstants.Id_guid,
                "/644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b");
        }

        public static string ForAccountGetUserByName(this string url)
        {
            return url + Regex.Replace(
                RouteConstants.GetUserByName,
                RouteConstants.UserName,
                "/name");
        }

        public static string ForAccountAssignRolesToUser(this string url)
        {
            return url + Regex.Replace(
                Regex.Replace(
                    RouteConstants.AssignRolesToUser,
                    RouteConstants.RepositoryId,
                    "/5"),
                RouteConstants.GitHubId,
                "/6");
        }

        public static string ForAccountGetExternalLogin(this string url)
        {
            return url + RouteConstants.GetExternalLogin +
                "?provider=p&error=e";
        }
    }
}
