using System.Threading.Tasks;
using System.Web.Http;

using GitHubExtension.Templates.Constants;
using GitHubExtension.Templates.ExtensionMethods;
using GitHubExtension.Templates.Services;

namespace GitHubExtension.Templates.Controllers
{
    [RoutePrefix(RouteTemplatesConstants.GetGitHubTemplatesRoute)]
    public class TemplatesController : BaseApiController
    {
        private const string PathToIssueTemplate = ".github/ISSUE_TEMPLATE.md";

        private const string PathToPullRequestTemplate = ".github/PULL_REQUEST_TEMPLATE.md";

        private const string RepositoryName = "repositoryName";

        private const string ReposytoryNameError = "Repository hasn't been choosen";

        private readonly ITemplateService _templateService;

        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

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
            var response = await _templateService.GetIssueTemplateAsync(userName, repositoryName, PathToIssueTemplate);

            var responseMessage = response.CheckResponseMessage();

            var content = await responseMessage.GetTemplatesContent();

            return Ok(content);
        }

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
                await _templateService.GetPullRequestTemplatesAsync(userName, repositoryName, PathToPullRequestTemplate);

            var responseMessage = response.CheckResponseMessage();

            var content = await responseMessage.GetTemplatesContent();

            return Ok(content);
        }
    }
}