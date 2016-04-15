using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Constant;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Exceptions;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Results;
using GitHubExtension.Security.WebApi.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Controllers
{
    [RoutePrefix(RouteConstants.ApiAccount)]
    public class AccountController : BaseApiController
    {
        #region private fields

        private readonly IGithubService _githubService;
        private readonly IContextActivityCommand _contextActivityCommand;
        private readonly IGetActivityTypeQuery _getActivityTypeQuery;
        private readonly ISecurityContext _securityContext;
        private readonly ApplicationUserManager _userManager;

        #endregion

        public AccountController(
            IGithubService githubService,
            IContextActivityCommand contextActivityCommand,
            IGetActivityTypeQuery getActivityTypeQuery,
            ISecurityContext securityContext,
            ApplicationUserManager userManager)
        {
            _githubService = githubService;
            _contextActivityCommand = contextActivityCommand;
            _getActivityTypeQuery = getActivityTypeQuery;
            _securityContext = securityContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        [Route(RouteConstants.GetUser, Name = "GetUserById")]
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
        [Authorize(Roles = RoleConstants.Admin)]
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

        [HttpPatch]
        [AllowAnonymous]
        [Route(RouteConstants.AssignRolesToUser)]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] int repoId, [FromUri] int gitHubId,
            [FromBody] string roleToAssign)
        {
            User appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.ProviderId == gitHubId);
            if (appUser == null)
                return NotFound();

            var repositoryRole = appUser.UserRepositoryRoles.FirstOrDefault(r => r.RepositoryId == repoId);

            if (repositoryRole != null)
                appUser.UserRepositoryRoles.Remove(repositoryRole);

            var role = await _securityContext.SecurityRoles.FirstOrDefaultAsync(r => r.Name == roleToAssign);
            if (role == null)
            {
                ModelState.AddModelError("role",
                    string.Format("Roles '{0}' does not exists in the system", roleToAssign));
                return BadRequest(ModelState);
            }

            appUser.UserRepositoryRoles.Add(new UserRepositoryRole()
            {
                RepositoryId = repoId,
                SecurityRoleId = role.Id
            });

            IdentityResult updateResult = await _userManager.UpdateAsync(appUser);

            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError("Role", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            var claimsIdentity =
                await appUser.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
            var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Value == repoId.ToString());
            if (existingClaim != null)
                _userManager.RemoveClaim(appUser.Id, existingClaim);
            var addClaimResult =
                await _userManager.AddClaimAsync(appUser.Id, new Claim(roleToAssign, repoId.ToString()));

            if (!addClaimResult.Succeeded)
            {
                ModelState.AddModelError("Role", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            var activityType = _getActivityTypeQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            _contextActivityCommand.AddActivity(new ActivityEvent
            {
                UserId = User.Identity.GetUserId(),
                CurrentRepositoryId = repoId,
                ActivityType = activityType,
                InvokeTime = DateTime.Now,
                Message =
                    String.Format("{0} {1} {2} to {3}", User.Identity.Name, activityType.Name, roleToAssign,
                        appUser.UserName)
            });

            return Ok();
        }

        [HttpGet]
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [Route(RouteConstants.GetExternalLogin, Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        // The first call, after click login with GitHub, and call when we write a info user
        {
            // if not allready authenticated sending user to GitHub
            if (!User.Identity.IsAuthenticated)
                return new ChallengeResult(provider, this);

            var claims = User.Identity as ClaimsIdentity;
            Claim tokenClaim = claims.FindFirst("ExternalAccessToken");

            //TODO: Log
            if (tokenClaim == null)
                throw new TokenNotFoundException();

            GitHubUserModel userReadModel = await _githubService.GetUserAsync(tokenClaim.Value);
            User user = _userManager.FindByGitHubId(userReadModel.GitHubId);

            if (user == null)
            {
                // if result is not null we have an error occured
                user = userReadModel.ToUserEntity();
                IHttpActionResult registrationResult = await RegisterUser(user, tokenClaim.Value);
                if (registrationResult != null)
                    return registrationResult;
            }

            ClaimsIdentity localIdentity =
                await user.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
            localIdentity.AddClaim(tokenClaim);

            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            authentication.SignIn(localIdentity);

            // TODO Need method for cookies in the future?
            HttpContext.Current.Response.Cookies["userName"].Value = user.UserName;
            HttpContext.Current.Response.Cookies["isAuth"].Value = "true";

            return Redirect("http://localhost:50859/");
        }

        [HttpPost]
        [Route("logout")]
        public IHttpActionResult LogOut()
        {
            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return Ok();
        }


        private async Task<IHttpActionResult> RegisterUser(User user, string token)
        {
            SecurityRole role = await _securityContext.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (role == null)
                return InternalServerError();


            List<GitHubRepositoryModel> repositories = await _githubService.GetReposAsync(token);

            var repositoryRolesToAdd =
                repositories.Select(r => new UserRepositoryRole() { Repository = r.ToEntity(), SecurityRole = role })
                    .ToList();
            user.UserRepositoryRoles = repositoryRolesToAdd;

            IdentityResult addUserResult = await _userManager.CreateAsync(user);
            if (!addUserResult.Succeeded)
                return GetErrorResult(addUserResult);

            if (repositories.Any(r => !_userManager
                .AddClaim(user.Id,
                    new Claim(role.Name, r.GitHubId.ToString())).Succeeded))
                return BadRequest();

            var userActivityType = _getActivityTypeQuery.GetUserActivityType(ActivityTypeNames.JoinToSystem);

            _contextActivityCommand.AddActivity(new ActivityEvent()
            {
                UserId = user.Id,
                ActivityType = userActivityType,
                InvokeTime = DateTime.Now,
                Message = String.Format("{0} {1}", User.Identity.Name, userActivityType.Name)
            });


            var repositoryActivityType = _getActivityTypeQuery.GetUserActivityType(ActivityTypeNames.RepositoryAddedToSystem);

            foreach (var repository in repositoryRolesToAdd)
            {
                _contextActivityCommand.AddActivity(new ActivityEvent()
                {
                    UserId = user.Id,
                    CurrentRepositoryId = repository.RepositoryId,
                    ActivityType = repositoryActivityType,
                    InvokeTime = DateTime.Now,
                    Message = String.Format("{0} {1}", repository.Repository.Name, repositoryActivityType.Name)
                });
            }

            return null;
        }
    }
}
