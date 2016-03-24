using System.Threading.Tasks;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.StorageModels.Identity;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Library.Services
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
