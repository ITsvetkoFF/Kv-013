using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Identity
{
    public class User : IdentityUser
    {
        public int ProviderId { get; set; }
        public string GitHubUrl { get; set; }

        public virtual ICollection<UserRepositoryRole> UserRepositoryRoles { get; set; }

        public User()
        {
            UserRepositoryRoles = new List<UserRepositoryRole>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
