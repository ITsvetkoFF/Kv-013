using System.Collections.Generic;
using GitHubExtension.Security.DAL.Entities;
using GitHubExtension.Security.WebApi.Models;
using System.Linq;

namespace GitHubExtension.Security.WebApi.Converters
{
    public static class RoleConverter
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