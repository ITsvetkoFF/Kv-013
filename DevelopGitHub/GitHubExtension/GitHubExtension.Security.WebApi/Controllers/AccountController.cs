using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Entities;
using GitHubExtension.Security.WebApi.Attributes;
using GitHubExtension.Security.WebApi.Converters;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace GitHubExtension.Security.WebApi.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        private IGithubService _githubService;

        private SecurityContext Context
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<SecurityContext>();
            }
        }

        public AccountsController()
        {
            //Context = new SecurityContext();

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

        [ClaimsAuthorization(ClaimType = "Role", ClaimValue = "Admin")]
        [Route("user/{userId:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] int reposId, [FromUri] string userId, [FromBody] string roleToAssign)
        {
            
            var appUser = await ApplicationUserManager.FindByIdAsync(userId);

            if (appUser == null)
            {
                return NotFound();
            }
          
            var role = await Context.SecurityRoles.FirstOrDefaultAsync(r => r.Name == roleToAssign);
            if (role == null)
            {
                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", roleToAssign));
                return BadRequest(ModelState);
            }

            var repositoryRole = appUser.UserRepositoryRoles.FirstOrDefault(r => r.RepositoryId == reposId);
            if (repositoryRole != null)
                appUser.UserRepositoryRoles.Remove(repositoryRole);
            appUser.UserRepositoryRoles.Add(new UserRepositoryRole() { RepositoryId = reposId, SecurityRoleId = role.Id});
            
            //Refreshing claim cookie need to check it
            var identity = await appUser.GenerateUserIdentityAsync(ApplicationUserManager, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim("Role", roleToAssign, reposId.ToString()));
            HttpContext.Current.GetOwinContext().Authentication.AuthenticationResponseGrant = new AuthenticationResponseGrant(identity, new AuthenticationProperties { IsPersistent = true });

            IdentityResult updateResult = await ApplicationUserManager.UpdateAsync(appUser);


            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [Route("create")]
        [HttpPost]
        //[Authorize]
        public async Task<IHttpActionResult> CreateUser(string token)
        {
            UserDto userDto = await _githubService.GetUserAsync(token);
            List<RepositoryDto> repos = await _githubService.GetReposAsync(userDto.Login, token);

            // TODO: ceck exists
            var role = await Context.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
            var repositoriesToAdd = repos.Select(r => new UserRepositoryRole() { Repository = r.ToEntity(), SecurityRole = role}).ToList();
            
            //TODO: Use converter
            var user = new User()
            {
                Email = userDto.Email,
                UserName = userDto.Login,
                Token = token,
                ProviderId = userDto.GitHubId,
                UserRepositoryRoles = repositoriesToAdd
            };

            IdentityResult addUserResult = await ApplicationUserManager.CreateAsync(user);
            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            
            // Change identity user to app user
            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));
            return Created(locationHeader, TheModelFactory.Create(user));
        }
    }
}