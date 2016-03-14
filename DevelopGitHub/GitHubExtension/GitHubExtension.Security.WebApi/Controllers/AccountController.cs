using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using GithubExtension.Security.DAL.Context;
using GithubExtension.Security.WebApi.Converters;
using GitHubExtension.Security.DAL.Entities;
using GitHubExtension.Security.WebApi.Attributes;
using GitHubExtension.Security.WebApi.Converters;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace GitHubExtension.Security.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        private IGithubService _githubService;

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private SecurityContext Context
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<SecurityContext>();
            }
        }

        public AccountsController()
        {
            _githubService = new GithubService();
        }

        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(ApplicationUserManager.Users.ToList().Select(u => TheModelFactory.Create(u)));
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await ApplicationUserManager.FindByIdAsync(id);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await ApplicationUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();
        }

        //Commented intentinaly, need to be tested with authorization logic
        //[ClaimsAuthorization(ClaimType = "Role", ClaimValue = "Admin")]
        
        [Route("api/repos/{repoId}/collaborators/{gitHubId}")]
        [HttpPatch]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] int repoId, [FromUri] int gitHubId, [FromBody] string roleToAssign)
        {
            var appUser = await ApplicationUserManager.Users.FirstOrDefaultAsync(u => u.ProviderId == gitHubId);
            if (appUser == null)
                return NotFound();

            var role = await Context.SecurityRoles.FirstOrDefaultAsync(r => r.Name == roleToAssign);
            if (role == null)
            {
                ModelState.AddModelError("role", string.Format("Roles '{0}' does not exists in the system", roleToAssign));
                return BadRequest(ModelState);
            }

            var repositoryRole = appUser.UserRepositoryRoles.FirstOrDefault(r => r.RepositoryId == repoId);
            if (repositoryRole != null)
                appUser.UserRepositoryRoles.Remove(repositoryRole);


            appUser.UserRepositoryRoles.Add(new UserRepositoryRole() { RepositoryId = repoId, SecurityRoleId = role.Id });
            IdentityResult updateResult = await ApplicationUserManager.UpdateAsync(appUser);

            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError("Role", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            var claimsIdentity = await appUser.GenerateUserIdentityAsync(ApplicationUserManager, DefaultAuthenticationTypes.ApplicationCookie);
            var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Value == repoId.ToString());
            if (existingClaim != null)
                ApplicationUserManager.RemoveClaim(appUser.Id, existingClaim);
            var addClaimResult = await ApplicationUserManager.AddClaimAsync(appUser.Id, new Claim(roleToAssign, repoId.ToString()));

            if (!addClaimResult.Succeeded)
            {
                ModelState.AddModelError("Role", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [Route("api/account/register")]
        [HttpPost]
        //[Authorize]
        public async Task<IHttpActionResult> CreateUser(string token)
        {
            var role = await Context.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (role == null)
                return InternalServerError();

            GitHubUserModel gitHubUserModel = await _githubService.GetUserAsync(token);
            List<RepositoryDto> repos = await _githubService.GetReposAsync(gitHubUserModel.Login, token);

            var repositoriesToAdd = repos.Select(r => new UserRepositoryRole() { Repository = r.ToEntity(), SecurityRole = role }).ToList();
            var user = gitHubUserModel.ToUserEntity();
            user.Token = token;
            user.UserRepositoryRoles = repositoriesToAdd;

            IdentityResult addUserResult = await ApplicationUserManager.CreateAsync(user);
            if (!addUserResult.Succeeded)
                return GetErrorResult(addUserResult);

            if (repos.Any(repo => !ApplicationUserManager.AddClaim(user.Id,
                new Claim(role.Name, repo.GitHubId.ToString())).Succeeded))
            {
                return BadRequest();
            }

            var identity = await user.GenerateUserIdentityAsync(ApplicationUserManager, DefaultAuthenticationTypes.ApplicationCookie);
            Authentication.SignOut();
            Authentication.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

            // Change identity user to app user
            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));
            return Created(locationHeader, TheModelFactory.Create(user));
        }
    }
}