using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Mappers
{
    public static class UserRepositoryRoleMapper
    {
        public static UserRepositoryRole ToUserRepositoryRole(this UpdateSecurityRoleModel securityRoleModel)
        {
            UserRepositoryRole userRepositoryRole = new UserRepositoryRole()
            {
                RepositoryId = securityRoleModel.RepositoryId,
                SecurityRoleId = securityRoleModel.SecurityRole.Id
            };
            return userRepositoryRole;
        }
    }
}
