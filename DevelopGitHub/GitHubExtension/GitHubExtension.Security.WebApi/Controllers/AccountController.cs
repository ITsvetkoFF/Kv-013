using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Exceptions;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Queries.Interfaces;
using GitHubExtension.Security.WebApi.Results;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Controllers
{
    [RoutePrefix(RouteConstants.ApiAccount)]
    public class AccountController : BaseApiController
    {
        private readonly IGitHubQuery _gitHubQuery;
        private readonly ISecurityContext _securityContext;
        private readonly ApplicationUserManager _userManager;

        public AccountController(
            IGitHubQuery gitHubQuery,
            ISecurityContext securityContext,
            ApplicationUserManager userManager)
        {
            _gitHubQuery = gitHubQuery;
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

            if (tokenClaim == null)
                throw new TokenNotFoundException();

            GitHubUserModel userReadModel = await _gitHubQuery.GetUserAsync(tokenClaim.Value);
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

            List<RepositoryViewModel> repositories = await _gitHubQuery.GetReposAsync(token);

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


            return null;
        }
    }
}
