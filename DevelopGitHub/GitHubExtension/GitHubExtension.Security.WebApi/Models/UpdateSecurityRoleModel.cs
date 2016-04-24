using GitHubExtension.Security.DAL.Identity;

namespace GitHubExtension.Security.WebApi.Models
{
    public class UpdateSecurityRoleModel
    {
        public int RepositoryId { get; set; }

        public SecurityRole SecurityRole { get; set; }
    }
}