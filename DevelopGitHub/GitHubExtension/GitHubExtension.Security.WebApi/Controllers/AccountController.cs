using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Infrastructure.Extensions.Identity;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.WebApi.Extensions.Cookie;
using GitHubExtension.Security.WebApi.Extensions.OwinContext;
using GitHubExtension.Security.WebApi.Extensions.SecurityContext;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Queries.Interfaces;
using GitHubExtension.Security.WebApi.Results;
using GitHubExtention.Preferences.WebApi;
using GitHubExtention.Preferences.WebApi.Queries;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Controllers
{
    [RoutePrefix(RouteConstants.ApiAccount)]
    public class AccountController : BaseApiController
    {
        private readonly IGitHubQuery _gitHubQuery;

        private readonly ISecurityContextQuery _securityContextQuery;

        private readonly ApplicationUserManager _userManager;

        private readonly IAzureContainerQuery _container;

        public AccountController(
            IGitHubQuery gitHubQuery, 
            ApplicationUserManager userManager, 
            ISecurityContextQuery securityContextQuery,
            IAzureContainerQuery container)
        {
            _gitHubQuery = gitHubQuery;
            _userManager = userManager;
            _securityContextQuery = securityContextQuery;
            _container = container;
        }

        [HttpGet]
        [AllowAnonymous]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [Route(RouteConstants.GetExternalLogin)]
        public async Task<IHttpActionResult> GetExternalLogin(string provider)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            Claim tokenClaim = User.Identity.GetExternalAccessTokenClaim();

            GitHubUserModel userReadModel = await _gitHubQuery.GetUserAsync(tokenClaim.Value);
            User user = _userManager.FindByGitHubId(userReadModel.GitHubId);
            if (user == null)
            {
                user = userReadModel.ToUserEntity();
                var file = await _gitHubQuery.GetAvatar(userReadModel.AvatarUrl);
                user.AvatarUrl = await _container.SaveAvatar(file);
                IHttpActionResult registrationResult = await RegisterUser(user, tokenClaim.Value);
                if (registrationResult != null)
                {
                    _container.DeleteBlob(user.AvatarUrl);
                    return registrationResult;
                }
            }

            await UpdateClaims(user, tokenClaim);

            GetRequestContext.SetUserCookie(user.UserName);
            string homeUri = ConfigurationManager.AppSettings[AppSettingConstants.Uri];
            return Redirect(homeUri);
        }

        [HttpGet]
        [Route(RouteConstants.MailVisibility)]
        public async Task<bool> MailVisibility()
        {
            string id = User.Identity.GetUserId();

            var user = await _userManager.FindByIdAsync(id);
            return user.IsMailVisible;
        }
                
        [HttpPut]
        [Route(RouteConstants.ChangeVisibilityMail)]
        public async Task<IHttpActionResult> ChangeVisibilityMail()
        {
            string id = User.Identity.GetUserId();

            var user = await _userManager.FindByIdAsync(id);
            user.IsMailVisible = !user.IsMailVisible;
            _userManager.Update(user);

            return Ok();
        }

        [HttpGet]
        [Route(RouteConstants.GetUser)]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                return Ok(user.ToUserReturnModel());
            }

            return NotFound();
        }

        [HttpGet]
        [Route(RouteConstants.GetUserByName)]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(user.ToUserReturnModel());
            }

            return NotFound();
        }

        [HttpPost]
        [Route(RouteConstants.AccountLogout)]
        public IHttpActionResult LogOut()
        {
            var authentication = GetRequestContext.Authentication();
            authentication.SignOut();

            return Ok();
        }

        private static void DeleteUserRepositoryRoles(int repoId, User appUser)
        {
            var repositoryRole = appUser.UserRepositoryRoles.FirstOrDefault(r => r.RepositoryId == repoId);

            if (repositoryRole != null)
            {
                appUser.UserRepositoryRoles.Remove(repositoryRole);
            }
        }

        private async Task<IHttpActionResult> AddUserClaim(int repoId, string roleToAssign, User appUser)
        {
            var claimsIdentity =
                await appUser.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
            var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Value == repoId.ToString());
            if (existingClaim != null)
            {
                _userManager.RemoveClaim(appUser.Id, existingClaim);
            }

            var addClaimResult =
                await _userManager.AddClaimAsync(appUser.Id, new Claim(roleToAssign, repoId.ToString()));
            
            if (!addClaimResult.Succeeded)
            {
                ModelState.AddModelError(RoleConstants.Role, RoleValidationConstants.FailedRemoveRole);
                return BadRequest(ModelState);
            }

            return null;
        }

        private async Task<IHttpActionResult> RegisterUser(User user, string token)
        {
            string userRole = RoleConstants.Admin;
            SecurityRole role = _securityContextQuery.GetUserRole(userRole);

            if (role == null)
            {
                return InternalServerError();
            }

            IList<GitHubRepositoryModel> repositories = await _gitHubQuery.GetReposAsync(token);
            IList<UserRepositoryRole> repositoriesToAdd =
                repositories.Select(r => new UserRepositoryRole() { Repository = r.ToEntity(), SecurityRole = role }).ToList();

            user.UserRepositoryRoles = repositoriesToAdd;
            IdentityResult addUserResult = await _userManager.CreateAsync(user);
            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            if (!AddRoleToClaims(user.Id, repositories, role))
            {
                return BadRequest();
            }

            return null;
        }

        private async Task UpdateClaims(User user, Claim tokenClaim)
        {
            ClaimsIdentity localIdentity =
                await user.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
            localIdentity.AddClaim(tokenClaim);

            var authentication = GetRequestContext.Authentication();

            authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            authentication.SignIn(localIdentity);
        }

        private bool AddRoleToClaims(string userId, IEnumerable<GitHubRepositoryModel> repositories, SecurityRole role)
        {
            return repositories.All(r => _userManager.AddClaim(userId, new Claim(role.Name, r.GitHubId.ToString())).Succeeded);
        }
    }
}