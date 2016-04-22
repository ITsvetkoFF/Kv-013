using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.WebApi.Exceptions;
using GitHubExtension.Security.WebApi.Extensions.Cookie;
using GitHubExtension.Security.WebApi.Extensions.OwinContext;
using GitHubExtension.Security.WebApi.Extensions.SecurityContext;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Queries.Interfaces;
using GitHubExtension.Security.WebApi.Results;
using GitHubExtension.Statistics.WebApi.Extensions.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;

namespace GitHubExtension.Security.WebApi.Controllers
{
    [RoutePrefix(RouteConstants.ApiAccount)]
    public class AccountController : BaseApiController
    {
        #region private fields
        private readonly IGitHubQuery _gitHubQuery;
        private readonly ApplicationUserManager _userManager;
        private readonly ISecurityContextQuery _securityContextQuery;
        #endregion

        public AccountController(
            IGitHubQuery gitHubQuery,
            ApplicationUserManager userManager,
            ISecurityContextQuery securityContextQuery)
        {
            _gitHubQuery = gitHubQuery;
            _userManager = userManager;
            _securityContextQuery = securityContextQuery;
        }

        public RequestContext GetRequestContext
        {
            get
            {
                var context = new HttpContextWrapper(HttpContext.Current);
                var routeData = HttpContext.Current.Request.RequestContext.RouteData;

                var requestContext = new RequestContext(context, routeData);
                return requestContext;
            }
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
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
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] int repoId, [FromUri] int gitHubId, [FromBody] string roleToAssign)
        {
            User appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.ProviderId == gitHubId);
            if (appUser == null)
                return NotFound();

            DeleteUserRepositoryRoles(repoId, appUser);
            SecurityRole role = _securityContextQuery.GetUserRole(roleToAssign);

            if (role == null)
            {
                ModelState.AddModelError(RoleConstants.Role, string.Format(RoleValidationConstants.RoleNull, roleToAssign));
                return BadRequest(ModelState);
            }

            UpdateSecurityRoleModel updateSecurityRoleModel = new UpdateSecurityRoleModel(){ RepositoryId = repoId,  SecurityRole = role};
            appUser.UserRepositoryRoles.Add(updateSecurityRoleModel.ToUserRepositoryRole());

            IdentityResult updateResult = await _userManager.UpdateAsync(appUser);

            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError(RoleConstants.Role, RoleValidationConstants.FailedRemoveRole);
                return BadRequest(ModelState);
            }
            await AddUserClaim(repoId, roleToAssign, appUser);
            return Ok();
        }

        private static void DeleteUserRepositoryRoles(int repoId, User appUser)
        {
            var repositoryRole = appUser.UserRepositoryRoles.FirstOrDefault(r => r.RepositoryId == repoId);

            if (repositoryRole != null)
                appUser.UserRepositoryRoles.Remove(repositoryRole);
        }

        private async Task<IHttpActionResult> AddUserClaim(int repoId, string roleToAssign, User appUser)
        {
            var claimsIdentity =
                await appUser.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
            var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Value == repoId.ToString());
            if (existingClaim != null)
                _userManager.RemoveClaim(appUser.Id, existingClaim);
            var addClaimResult =
                await _userManager.AddClaimAsync(appUser.Id, new Claim(roleToAssign, repoId.ToString()));

            if (!addClaimResult.Succeeded)
            {
                ModelState.AddModelError(RoleConstants.Role, RoleValidationConstants.FailedRemoveRole);
                return BadRequest(ModelState);
            }

            return null;
        }

        [HttpGet]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [Route(RouteConstants.GetExternalLogin)]
        public async Task<IHttpActionResult> GetExternalLogin(string provider)
        {
            if (!User.Identity.IsAuthenticated)
                return new ChallengeResult(provider, this);

            Claim tokenClaim = User.Identity.GetClaim();
            if (tokenClaim == null)
                throw new TokenNotFoundException();

            GitHubUserModel userReadModel = await _gitHubQuery.GetUserAsync(tokenClaim.Value);
            User user = _userManager.FindByGitHubId(userReadModel.GitHubId);

            if (user == null)
            {
                user = userReadModel.ToUserEntity();
                IHttpActionResult registrationResult = await RegisterUser(user, tokenClaim.Value);
                if (registrationResult != null) return registrationResult;
            }

            await UpdateClaims(user, tokenClaim);

            GetRequestContext.SetUserCookie(user.UserName);
            string homeUri = ConfigurationManager.AppSettings[AppSettingConstants.Uri];
            return Redirect(homeUri);
        }

        [HttpPost]
        [Route(RouteConstants.AccountLogout)]
        public IHttpActionResult LogOut()
        {
            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignOut();

            return Ok();
        }

        private async Task<IHttpActionResult> RegisterUser(User user, string token)
        {
            string userRole = RoleConstants.Admin;
            SecurityRole role = _securityContextQuery.GetUserRole(userRole);

            if (role == null)
                return InternalServerError();

            IList<RepositoryViewModel> repositories = await _gitHubQuery.GetReposAsync(token);

            IList<UserRepositoryRole> repositoriesToAdd = repositories
                .Select(r => new UserRepositoryRole() { Repository = r.ToEntity(), SecurityRole = role })
                .ToList();

            user.UserRepositoryRoles = repositoriesToAdd;
            IdentityResult addUserResult = await _userManager.CreateAsync(user);
            if (!addUserResult.Succeeded)
                return GetErrorResult(addUserResult);

            if (repositories.Any(r => !_userManager
                    .AddClaim(user.Id,
                        new Claim(role.Name, r.GitHubId.ToString())).Succeeded))
                return BadRequest();

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
    }
}
