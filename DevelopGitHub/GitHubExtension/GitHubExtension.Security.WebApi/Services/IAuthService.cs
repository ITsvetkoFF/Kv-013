using System.Threading.Tasks;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Models;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(UserModel userModel);
        Task<User> FindUser(string userName, string password);
        Task<User> FindAsync(UserLoginInfo loginInfo);
        Task<IdentityResult> CreateAsync(User user);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
    }
}