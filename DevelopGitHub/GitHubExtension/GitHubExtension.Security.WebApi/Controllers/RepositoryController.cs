using System.Data.Entity;
using System.Linq;
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
    //[System.Web.Mvc.RoutePrefix("api")]
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
        [Route("repos/{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var repository = await Context.Repositories.FirstOrDefaultAsync(r => r.GitHubId == id);
            if (repository == null)
                return NotFound();

            return Ok(repository.ToRepositoryViewModel());
        }

        //[Authorize]
        [Route("api/user/repos")]
        public async Task<IHttpActionResult> GetReposForCurrentUser()
        {
            //var userId = User.Identity.GetUserId();
            var userId = "0d7c8801-b009-45a3-8e17-3fc4b7f56aca";
            var role = await Context.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (role == null)
                return NotFound();

            var repos = Context.Repositories
                .Where(r => r.UserRepositoryRoles.Any(ur => ur.UserId == userId && ur.SecurityRoleId == role.Id))
                .AsEnumerable().Select(r => r.ToRepositoryViewModel());
            return Ok(repos);
        }

        //[Authorize]
        [Route("api/repos/{repoName}/collaborators")]
        public async Task<IHttpActionResult> GetCollaboratorsForRepo(string repoName)
        {
            var userId = "0d7c8801-b009-45a3-8e17-3fc4b7f56aca";
            var user = await ApplicationUserManager.FindByIdAsync(userId);
            //var userName = User.Identity.GetUserName();
            

            var gitHubCollaborators = await _guGithubService.GetCollaboratorsForRepo(user.UserName, repoName, user.Token);

            return Ok(gitHubCollaborators);
        }
    }
}