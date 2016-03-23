using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using GitHubExtension.Domain.Interfaces;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Converters;
using GitHubExtension.Security.WebApi.Library.Controllers;
using GitHubExtension.Security.WebApi.Library.Converters;
using GitHubExtension.Security.WebApi.Library.Exceptions;
using GitHubExtension.Security.WebApi.Library.Results;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        #region private fields
        private readonly IGithubService _githubService;
        private readonly ISecurityContext _securityContext;
        private readonly IAuthService _authService;
        private readonly ApplicationUserManager _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IRoleStore<IdentityRole, string> _rolestore;

        #endregion

        public AccountController(
            IGithubService githubService,
            ISecurityContext securityContext,
            IAuthService authService,
            ApplicationUserManager userManager,
            IUserStore<User> userStore,
            IRoleStore<IdentityRole, string> rolestore)
        {
            _userStore = userStore;
            _rolestore = rolestore;
            _githubService = githubService;
            _securityContext = securityContext;
            _authService = authService;
            _userManager = userManager;
            
        }

        //[Authorize(Roles = "Admin")]
        //[Route("user/{id:guid}", Name = "GetUserById")]
        //public async Task<IHttpActionResult> GetUser(string id)
        //{
        //    var user = await ApplicationUserManager.FindByIdAsync(id);

        //    if (user != null)
        //    {
        //        return Ok(TheModelFactory.Create(user));
        //    }

        //    return NotFound();
        //}

        //[Authorize(Roles = "Admin")]
        //[Route("user/{username}")]
        //public async Task<IHttpActionResult> GetUserByName(string username)
        //{
        //    var user = await ApplicationUserManager.FindByNameAsync(username);

        //    if (user != null)
        //    {
        //        return Ok(TheModelFactory.Create(user));
        //    }

        //    return NotFound();
        //}

        ////Commented intentinaly, need to be tested with authorization logic
        ////[ClaimsAuthorization(ClaimType = "Role", ClaimValue = "Admin")]
        //[Route("api/repos/{repoId}/collaborators/{gitHubId}")]
        //[HttpPatch]
        //public async Task<IHttpActionResult> AssignRolesToUser([FromUri] int repoId, [FromUri] int gitHubId, [FromBody] string roleToAssign)
        //{
        //    var appUser = await ApplicationUserManager.Users.FirstOrDefaultAsync(u => u.ProviderId == gitHubId);
        //    if (appUser == null)
        //        return NotFound();

        //    var role = await _securityContext.SecurityRoles.FirstOrDefaultAsync(r => r.Name == roleToAssign);
        //    if (role == null)
        //    {
        //        ModelState.AddModelError("role", string.Format("Roles '{0}' does not exists in the system", roleToAssign));
        //        return BadRequest(ModelState);
        //    }

        //    var repositoryRole = appUser.UserRepositoryRoles.FirstOrDefault(r => r.RepositoryId == repoId);
        //    if (repositoryRole != null)
        //        appUser.UserRepositoryRoles.Remove(repositoryRole);


        //    appUser.UserRepositoryRoles.Add(new UserRepositoryRole()
        //    {
        //        RepositoryId = repoId,
        //        SecurityRoleId = role.Id
        //    });
        //    IdentityResult updateResult = await ApplicationUserManager.UpdateAsync(appUser);

        //    if (!updateResult.Succeeded)
        //    {
        //        ModelState.AddModelError("Role", "Failed to remove user roles");
        //        return BadRequest(ModelState);
        //    }

        //    var claimsIdentity = await appUser.GenerateUserIdentityAsync(ApplicationUserManager, DefaultAuthenticationTypes.ApplicationCookie);
        //    var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Value == repoId.ToString());
        //    if (existingClaim != null)
        //        ApplicationUserManager.RemoveClaim(appUser.Id, existingClaim);
        //    var addClaimResult = await ApplicationUserManager.AddClaimAsync(appUser.Id, new Claim(roleToAssign, repoId.ToString()));

        //    if (!addClaimResult.Succeeded)
        //    {
        //        ModelState.AddModelError("Role", "Failed to remove user roles");
        //        return BadRequest(ModelState);
        //    }

        //    return Ok();
        //}

        //// POST api/Account/RegisterExternal
        //[AllowAnonymous]
        //[Route("RegisterExternal")]
        //public async Task<IHttpActionResult> CreateUser(string token)
        //{
        //    var role = await _securityContext.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
        //    if (role == null)
        //        return InternalServerError();

        //    GitHubUserModel gitHubUserModel = await _githubService.GetUserAsync(token);
        //    List<RepositoryDto> repos = await _githubService.GetReposAsync(gitHubUserModel.Login, token);

        //    var repositoriesToAdd = repos.Select(r => new UserRepositoryRole() { Repository = r.ToEntity(), SecurityRole = role }).ToList();
        //    var user = gitHubUserModel.ToUserEntity();
        //    user.Token = token;
        //    user.UserRepositoryRoles = repositoriesToAdd;

        //    IdentityResult addUserResult = await ApplicationUserManager.CreateAsync(user);
        //    if (!addUserResult.Succeeded)
        //        return GetErrorResult(addUserResult);

        //    if (repos.Any(repo => !ApplicationUserManager.AddClaim(user.Id,
        //        new Claim(role.Name, repo.GitHubId.ToString())).Succeeded))
        //    {
        //        return BadRequest();
        //    }

        //    var identity = await user.GenerateUserIdentityAsync(ApplicationUserManager, DefaultAuthenticationTypes.ApplicationCookie);
        //    _authenticationManager.SignOut();
        //    _authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

        //    // Change identity user to app user
        //    Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));
        //    return Created(locationHeader, TheModelFactory.Create(user));
        //}


        // GET api/Account/ExternalLogin
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="error"></param>
        /// <returns></returns>

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)// The first call, after click login with GitHub, and call when we write a info user
        {
            // if not allready authenticated sending user to GitHub
            if (!User.Identity.IsAuthenticated)
                return new ChallengeResult(provider, this);

            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");

            //TODO: Log
            if (token == null)
                throw new TokenNotFoundException();

            GitHubUserModel userReadModel = await _githubService.GetUserAsync(token);
            User user = _userManager.FindByGitHubId(userReadModel.GitHubId);

            if (user == null)
            {
                #region move to service
                SecurityRole role = await _securityContext.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
                if (role == null)
                    return InternalServerError();
                
                GitHubUserModel gitHubUserModel = await _githubService.GetUserAsync(token);
                List<RepositoryDto> repositories = await _githubService.GetReposAsync(token);
                var repositoriesToAdd = repositories.Select(r => new UserRepositoryRole() { Repository = r.ToEntity(), SecurityRole = role }).ToList();
                user = gitHubUserModel.ToUserEntity();
                user.UserRepositoryRoles = repositoriesToAdd;
                
                IdentityResult addUserResult = await _userManager.CreateAsync(user);
                if (!addUserResult.Succeeded)
                    return GetErrorResult(addUserResult);

                if (repositories.Any(r => !_userManager
                        .AddClaim(user.Id,
                            new Claim(role.Name, r.GitHubId.ToString())).Succeeded))
                    return BadRequest();
                #endregion
            }

            HttpContext.Current.GetOwinContext().Authentication.SignIn(await user.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie));
            return Redirect("http://localhost:3000/");
        }
    }
}