using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Library.Mappers;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Library.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class RepositoryController : BaseApiController
    {
        private IGithubService _guGithubService;
        private readonly ISecurityContext _securityContext;

        public RepositoryController(IGithubService guGithubService, ISecurityContext securityContext)
        {
            _guGithubService = guGithubService;
            _securityContext = securityContext;
        }

        // GET: api/repos/:id
        // id is a GitHub id of a repo
        [Route("repos/{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var repository = await _securityContext.Repositories.FirstOrDefaultAsync(r => r.GitHubId == id);
            if (repository == null)
                return NotFound();

            return Ok(repository.ToRepositoryViewModel());
        }

        [Authorize]
        [Route("api/user/repos")]
        public async Task<IHttpActionResult> GetReposForCurrentUser()
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

        [Authorize]
        [Route("api/repos/{repoName}/collaborators")]
        public async Task<IHttpActionResult> GetCollaboratorsForRepo(string repoName)
        {
            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");
            string userName = User.Identity.GetUserName();

            var gitHubCollaborators = await _guGithubService.GetCollaboratorsForRepo(userName, repoName, token);

            return Ok(gitHubCollaborators);
        }
    }
}