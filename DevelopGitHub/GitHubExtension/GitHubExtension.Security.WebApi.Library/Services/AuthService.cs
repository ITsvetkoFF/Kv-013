using System.Threading.Tasks;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Library.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISecurityContext _securityContext;
        private readonly ApplicationUserManager _userManager;
        private readonly IGithubService _githubService;

        public AuthService(IGithubService githubService,  ApplicationUserManager userManager, ISecurityContext context)
        {
            _securityContext = context;
            _userManager = userManager;
            _githubService = githubService;
        }
        
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            User user = new User
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await _userManager.FindAsync(userName, password);

            return user;
        }

        //TODO: Use GitHub id 
        public async Task<User> FindAsync(UserLoginInfo loginInfo)
       {
            User user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }
    }
}