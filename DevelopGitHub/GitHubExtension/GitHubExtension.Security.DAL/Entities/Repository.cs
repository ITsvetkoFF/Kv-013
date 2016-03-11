using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace GitHubExtension.Security.DAL.Entities
{
    public class Repository
    {
        public int Id { get; set; }
        public int GitHubId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<UserRepositoryRole> UserRepositoryRoles { get; set; }

        public Repository()
        {
            UserRepositoryRoles = new List<UserRepositoryRole>();
        }
    }
}
