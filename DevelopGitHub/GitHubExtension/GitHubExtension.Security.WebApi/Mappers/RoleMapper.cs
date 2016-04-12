using System.Collections.Generic;
using System.Linq;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Mappers
{
    public static class RoleMapper
    {
        public static RoleViewModel ToRoleViewModel(this SecurityRole role)
        {
            return new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static IEnumerable<RoleViewModel> ToRoleViewModel(this IEnumerable<SecurityRole> securityRoles)
        {
            return securityRoles.Select(address => address.ToRoleViewModel());
        }
    }
}