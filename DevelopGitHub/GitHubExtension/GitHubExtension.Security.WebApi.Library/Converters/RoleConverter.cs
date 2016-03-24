using System.Collections.Generic;
using System.Linq;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.StorageModels.Identity;

namespace GitHubExtension.Security.WebApi.Library.Converters
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