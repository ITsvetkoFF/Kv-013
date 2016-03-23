using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GitHubExtension.Domain.Interfaces;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Converters;
using GitHubExtension.Security.WebApi.Library.Controllers;
using GitHubExtension.Security.WebApi.Library.Converters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.WebApi.Library.Services
{
    public class AuthService : 
        IAuthService,
        IDisposable
    {
        private readonly ISecurityContext _securityContext;
        private readonly ApplicationUserManager _userManager;
        private readonly IGithubService _githubService;

        public AuthService(IGithubService githubService,  ApplicationUserManager userManager, ISecurityContext context)
        {
            this._securityContext = context;
            this._userManager = userManager;
            this._githubService = githubService;
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

        public Client FindClient(string clientId)
        {
            var client = _securityContext.Clients.Find(clientId);

            return client;
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

        public void Dispose()
        {
            //TODO: Refactor dispose
            //_ctx.Dispose();
            _userManager.Dispose();
        }
    }
}