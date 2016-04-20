using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Controllers
{
    public class RepositoryController : BaseApiController
    {
        private const string UserDoesNotHaveRepository = "User with id {0} don't have a repository with id {1} in system";

        private IGithubService _guGithubService;
        private readonly ISecurityContext _securityContext;
        private readonly ApplicationUserManager _userManager;

        public RepositoryController(IGithubService guGithubService, ISecurityContext securityContext, ApplicationUserManager userManager)
        {
            _guGithubService = guGithubService;
            _securityContext = securityContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Route(RouteConstants.GetByIdRepository)]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var repository = await _securityContext.Repositories.FirstOrDefaultAsync(r => r.GitHubId == id);
            if (repository == null)
                return NotFound();

            return Ok(repository.ToRepositoryViewModel());
        }

        [HttpGet]
        [Authorize]
        [Route(RouteConstants.GetRepositoryForCurrentUser)]
        public async Task<IHttpActionResult> GetRepositoryForCurrentUser()
        {
            string userId = User.Identity.GetUserId();
            var role = await _securityContext.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (role == null)
                return NotFound();

            var repos = _securityContext.Repositories
                .Where(r => r.UserRepositoryRoles.Any(ur => ur.UserId == userId && ur.SecurityRoleId == role.Id))
                .AsEnumerable().Select(r => r.ToRepositoryViewModel());
            return Ok(repos);
        }

        [HttpGet]
        [Authorize]
        [Route(RouteConstants.GetCollaboratorsForRepository)]
        public async Task<IHttpActionResult> GetCollaboratorsForRepo(string repoName)
        {
            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");
            string userName = User.Identity.GetUserName();

            var gitHubCollaborators = await _guGithubService.GetCollaboratorsForRepo(userName, repoName, token);

            return Ok(gitHubCollaborators);
        }

        [Authorize]
        [Route("api/repos/current")]
        [HttpPatch]
        //TODO: use DTO
        public async Task<IHttpActionResult> UpdateProject(Repository repo)
        {
            string currentUserId = User.Identity.GetUserId();
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
            if (user == null)
                return NotFound();

            var userRepositoryRole = _securityContext.UserRepository.FirstOrDefault(r => r.RepositoryId == repo.Id && r.UserId == user.Id);
            if (userRepositoryRole == null)
            {
                ModelState.AddModelError("repo", string.Format(UserDoesNotHaveRepository, user.Id, repo.Id));
                return BadRequest(ModelState);
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            Claim[] claimsToAdd =
            {
                new Claim("CurrentProjectId", repo.Id.ToString()),
                new Claim("CurrentProjectName", userRepositoryRole.Repository.FullName)
            };

            RemoveClaims(claimsIdentity, claimsToAdd, user.Id);
            var results = AddClaims(claimsIdentity, claimsToAdd, user.Id);
                
            if (results.Any(r => !r.Succeeded))
            {
                ModelState.AddModelError("CurrentProject", "Failed to update current project");
                return BadRequest(ModelState);
            }

            //Updating claim cookie
            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignOut();
            authentication.SignIn(claimsIdentity);

            return Ok();
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

            return Ok();
        }

        private void RemoveClaims(ClaimsIdentity claimsIdentity, Claim[] claims, string userId)
        {
            foreach (var claim in claims)
            {
                var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == claim.Type);
                if (existingClaim != null)
                {
                    _userManager.RemoveClaim(userId, existingClaim);
                    claimsIdentity.RemoveClaim(existingClaim);
                }
            }
        }

        private IdentityResult[] AddClaims(ClaimsIdentity claimsIdentity, Claim[] claimsToAdd, string userId)
        {
            IdentityResult[] results = new IdentityResult[claimsToAdd.Length];

            for (int i = 0; i < claimsToAdd.Length; i++)
            {
                results[i] = _userManager.AddClaim(userId, claimsToAdd[i]);
                claimsIdentity.AddClaim(claimsToAdd[i]);
            }

            return results;
        }
    }
}