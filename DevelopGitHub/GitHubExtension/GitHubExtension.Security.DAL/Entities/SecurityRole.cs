

using System.Collections.Generic;

namespace GitHubExtension.Security.DAL.Entities
{
    public class SecurityRole 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRepositoryRole> UserRepositoryRoles { get; set; }

        public SecurityRole()
        {
            UserRepositoryRoles = new List<UserRepositoryRole>();
        }
    }
}
