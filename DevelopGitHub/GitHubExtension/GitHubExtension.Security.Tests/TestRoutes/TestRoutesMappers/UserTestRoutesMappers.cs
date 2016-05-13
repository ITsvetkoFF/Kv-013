using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GitHubExtension.Infrastructure.Constants;

namespace GitHubExtension.Security.Tests.TestRoutes.TestRoutesMappers
{
    public static class UserTestRoutesMappers
    {
        public static string ForGetAllUsersByName(this string url)
        {
            return url + RouteConstants.SearchUsersByName + "?username=test";
        }
    }
}
