using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Templates.Constants;
using GitHubExtension.Templates.Exceptions;
using GitHubExtension.Templates.ExtensionMethods;
using GitHubExtension.Templates.Services;

namespace GitHubExtension.Templates.Controllers
{
    [RoutePrefix(RouteTemplatesConstants.GetGitHubTemplatesRoute)]
    public class TemplatesController : BaseApiController
    {
        private readonly ITemplateService _templateService;
        private const string PathToPullRequestTemplate = ".github/PULL_REQUEST_TEMPLATE.md";
        private const string PathToIssueTemplate = ".github/ISSUE_TEMPLATE.md";
        private const string RepositoryName = "repositoryName";
        private const string ReposytoryNameError = "Repository hasn't been choosen";

        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }
        [Authorize]
        [HttpGet]
        [Route(RouteTemplatesConstants.PullRequestTemplate)]
        public async Task<IHttpActionResult> GetPullRequestTemplate()
        {
            var repositoryName = User.GetCurrentProjectName();
            if (repositoryName == null)
            {
                ModelState.AddModelError(RepositoryName, ReposytoryNameError);
                return BadRequest(ModelState);
            }
            var userName = User.Identity.Name;
            var response =
                await
                    _templateService.GetPullRequestTemplatesAsync(userName, repositoryName,
                        PathToPullRequestTemplate);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();

            if (!response.IsSuccessStatusCode)
                throw new UnsuccessfullGitHubRequestException();

            var content = await response.GetTemplatesContent();

            return Ok(content);
        }
        [Authorize]
        [HttpGet]
        [Route(RouteTemplatesConstants.IssueTemplate)]
        public async Task<IHttpActionResult> GetIssueTemplate()
        {
            var repositoryName = User.GetCurrentProjectName();
            if (repositoryName == null)
            {
                ModelState.AddModelError(RepositoryName, ReposytoryNameError);
                return BadRequest(ModelState);
            }
            var userName = User.Identity.Name;
            var response =
                await
                    _templateService.GetIssueTemplateAsync(userName, repositoryName,
                    PathToIssueTemplate);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();

            if (!response.IsSuccessStatusCode)
                throw new UnsuccessfullGitHubRequestException();

            var content = await response.GetTemplatesContent();

            return Ok(content);
        }
    }
}