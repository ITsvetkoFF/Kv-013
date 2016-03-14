using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using GithubExtension.Security.DAL.Context;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using GitHubExtension.Security.WebApi.Converters;
using GitHubExtension.Security.WebApi.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Controllers
{
    [System.Web.Mvc.RoutePrefix("api/repos")]
    public class RepositoryController : BaseApiController
    {
        private IGithubService _guGithubService;
        private SecurityContext Context
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<SecurityContext>();
            }
        }

        public RepositoryController()
        {
            _guGithubService = new GithubService();
        }

        public RepositoryController(IGithubService guGithubService)
        {
            _guGithubService = guGithubService;
        }

        // GET: api/repos/:id
        // id is a GitHub id of a repo
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var repository = await Context.Repositories.FirstOrDefaultAsync(r => r.GitHubId == id);
            if (repository == null)
                return NotFound();

            return Ok(repository.ToRepositoryViewModel());
        }

        [Authorize]
        [Route("")]
        public async Task<IHttpActionResult> GetReposForCurrentUser()
        {
            var userId = User.Identity.GetUserId();
            var role = await Context.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (role == null)
                return NotFound();

            var repos = Context.Repositories
                .Where(r => r.UserRepositoryRoles.Any(ur => ur.UserId == userId && ur.SecurityRoleId == role.Id))
                .AsEnumerable().Select(r => r.ToRepositoryViewModel());
            return Ok(repos);
        }

        //[Authorize]
        [Route("{repoName}/collaborators")]
        public async Task<IHttpActionResult> GetCollaboratorsForRepo(string repoName, string token, string username)
        {
            //var userName = User.Identity.GetUserName();
            

            var gitHubCollaborators = await _guGithubService.GetCollaboratorsForRepo(username, repoName, token);

            return Ok(gitHubCollaborators);
        }
    }
}