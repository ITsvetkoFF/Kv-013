using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Threading.Tasks;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Library.Mappers;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;
using GitHubExtension.Constant;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.StorageModels.Identity;

namespace GitHubExtension.Security.WebApi.Library.Controllers
{
    public class RepositoryController : BaseApiController
    {
        private IGithubService _guGithubService;
        private readonly ISecurityContext _securityContext;
        private readonly ApplicationUserManager _userManager;

        public RepositoryController(IGithubService guGithubService, ISecurityContext securityContext,
            ApplicationUserManager userManager)
        {
            _guGithubService = guGithubService;
            _securityContext = securityContext;
            _userManager = userManager;
        }

        // id is a GitHub id of a repo
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
        public async Task<IHttpActionResult> GetCollaboratorsForRepo(string repositoryName)
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            var gitHubCollaborators = await _guGithubService.GetCollaboratorsForRepo(userName, repositoryName, token);

            return Ok(gitHubCollaborators);
        }

        [Authorize]
        [Route("api/repos/current")]
        [HttpPatch]
        public async Task<IHttpActionResult> UpdateProject(Repository repo)
        {
            string currentUserId = User.Identity.GetUserId();
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
            if (user == null)
                return NotFound();

            var repository = _securityContext.UserRepository.FirstOrDefault(r => r.RepositoryId == repo.Id && r.UserId == user.Id);
            if (repository == null)
            {
                ModelState.AddModelError("repo", string.Format("user with id '{0}' does not have a repository with id '{1}'", user.Id, repo.Id));
                return BadRequest(ModelState);
            }

            var claimsIdentity = await user.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
            var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "CurrentProjectId");
            if (existingClaim != null)
                _userManager.RemoveClaim(user.Id, existingClaim);

            var addClaimResult = await _userManager.AddClaimAsync(user.Id, new Claim("CurrentProjectId", repo.Id.ToString()));

            if (!addClaimResult.Succeeded)
            {
                ModelState.AddModelError("CurrentpProject", "Failed to update current project");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}