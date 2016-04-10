using System.Collections.Generic;

namespace GitHubExtension.Security.StorageModels.Identity
{
    public class Repository
    {
        public int Id { get; set; }
        public int GitHubId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<UserRepositoryRole> UserRepositoryRoles { get; set; }

        public Repository()
        {
            UserRepositoryRoles = new List<UserRepositoryRole>();
        }
    }
}
