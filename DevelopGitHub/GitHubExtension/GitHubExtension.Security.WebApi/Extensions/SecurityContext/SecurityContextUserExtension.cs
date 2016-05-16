using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Queries.Interfaces;

namespace GitHubExtension.Security.WebApi.Extensions.SecurityContext
{
    public static class SecurityContextUserExtension
    {
        public static IEnumerable<User> GetAllUsers(this ISecurityContextQuery securityContextQuery)
        {
            var users = securityContextQuery.Users.AsNoTracking();
            return users;
        }

        public static IEnumerable<User> GetUsersByNameExceptCurrent(
            this ISecurityContextQuery securityContextQuery,
            string searchName,
            string currentUserName)
        {
            IEnumerable<User> users =
                securityContextQuery.Users.Where(u => u.UserName.Contains(searchName) && u.UserName != currentUserName)
                    .AsNoTracking();
            return users;
        }
    }
}
