using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Queries.Interfaces;

namespace GitHubExtension.Security.WebApi.Extensions.SecurityContext
{
    public static class SecurityContextUserExtension
    {
        public static IEnumerable<User> GetUsersByName(this ISecurityContextQuery securityContextQuery, string userName)
        {
            IEnumerable<User> users =
                securityContextQuery.Users.Select(u => u).Where(u => u.UserName.Contains(userName)).AsNoTracking();
            return users;
        }
    }
}
