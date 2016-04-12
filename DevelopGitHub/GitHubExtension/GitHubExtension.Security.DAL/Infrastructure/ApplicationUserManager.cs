using System.Linq;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace GitHubExtension.Security.DAL.Infrastructure
{
    public class ApplicationUserManager : UserManager<User>, IApplicationUserManager
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            SecurityContext db = context.Get<SecurityContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<User>(db));

            return manager;
        }

        public User FindByGitHubId(int gitHubId)
        {
            return Users.FirstOrDefault(u => u.ProviderId == gitHubId);
        }

        ApplicationUserManager IApplicationUserManager.Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            return Create(options, context);
        }
    }
}
