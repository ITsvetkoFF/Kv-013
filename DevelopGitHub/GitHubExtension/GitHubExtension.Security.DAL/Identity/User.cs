using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Identity
{
    public class User : IdentityUser
    {
        public User()
        {
            UserRepositoryRoles = new List<UserRepositoryRole>();
        }

        public string GitHubUrl { get; set; }

        public int ProviderId { get; set; }

        public bool IsMailVisible { get; set; }

        public override string Email
        {
            get
            {
                return IsMailVisible ? base.Email : null;
            }

            set
            {
                base.Email = value;
            }
        }

        public virtual ICollection<UserRepositoryRole> UserRepositoryRoles { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<User> manager, 
            string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}