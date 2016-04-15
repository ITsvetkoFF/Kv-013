using System.Threading.Tasks;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Models;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISecurityContext securityContext;
        private readonly ApplicationUserManager userManager;
        private readonly IGithubService githubService;

        public AuthService(IGithubService githubService,  ApplicationUserManager userManager, ISecurityContext context)
        {
            this.securityContext = context;
            this.userManager = userManager;
            this.githubService = githubService;
        }
        
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            User user = new User
            {
                UserName = userModel.UserName
            };

            var result = await this.userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await this.userManager.FindAsync(userName, password);

            return user;
        }

        // TODO: Use GitHub id 
        public async Task<User> FindAsync(UserLoginInfo loginInfo)
       {
            User user = await this.userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            var result = await this.userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await this.userManager.AddLoginAsync(userId, login);

            return result;
        }
    }
}