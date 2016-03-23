using System.Security.Claims;
using System.Threading.Tasks;
using GitHubExtension.Security.StorageModels.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace GitHubExtension.Security.DAL.Infrastructure
{
    public interface IApplicationUserManager
    {
        ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context);
    }
}
