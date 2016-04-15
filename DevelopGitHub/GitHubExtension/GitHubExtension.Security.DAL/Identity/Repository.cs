using System.Collections.Generic;

namespace GitHubExtension.Security.DAL.Identity
{
    public class Repository
    {
        public Repository()
        {
            UserRepositoryRoles = new List<UserRepositoryRole>();
        }

        public int Id { get; set; }

        public int GitHubId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public virtual ICollection<UserRepositoryRole> UserRepositoryRoles { get; set; }
    }
}
