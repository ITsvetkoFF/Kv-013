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

namespace GitHubExtension.Security.WebApi.Library.Controllers
{
    public class RepositoryController : BaseApiController
    {
        private IGithubService _guGithubService;
        private readonly ISecurityContext _securityContext;

        public RepositoryController(IGithubService guGithubService, ISecurityContext securityContext)
        {
            _guGithubService = guGithubService;
            _securityContext = securityContext;
        }

        // GET
        // id is a GitHub id of a repo
        [Route(RouteConstant.getByIdRepository)]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var repository = await _securityContext.Repositories.FirstOrDefaultAsync(r => r.GitHubId == id);
            if (repository == null)
                return NotFound();

            return Ok(repository.ToRepositoryViewModel());
        }

        // GET
        [Authorize]
        [Route(RouteConstant.getRepositoryForCurrentUser)]
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

        // GET
        [Authorize]
        [Route(RouteConstant.getCollaboratorsForRepository)]
        public async Task<IHttpActionResult> GetCollaboratorsForRepo(string repositoryName)
        {
            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");
            string userName = User.Identity.GetUserName();

            var gitHubCollaborators = await _guGithubService.GetCollaboratorsForRepo(userName, repositoryName, token);

            return Ok(gitHubCollaborators);
        }
    }
}