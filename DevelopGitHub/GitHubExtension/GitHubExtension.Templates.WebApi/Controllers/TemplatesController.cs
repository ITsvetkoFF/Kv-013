using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Templates.WebApi.Constants;
using GitHubExtension.Templates.WebApi.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Templates.WebApi.Controllers
{
    [RoutePrefix(RouteTemplatesConstants.GetGitHubTemplatesRoute)]
    public class TemplatesController : BaseApiController
    {
        #region private fields

        private readonly ITemplateService _templateService;
        private const string CurrentProjectName = "CurrentProjectName";
        private const string PathToPullRequestTemplate = ".github/PULL_REQUEST_TEMPLATE.md";
        private const string PathToIssueTemplate = ".github/ISSUE_TEMPLATE.md";
        #endregion

        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [Route(RouteTemplatesConstants.PullRequestTemplate)]
        public async Task<IHttpActionResult> GetPullRequestTemplate()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var repositoryName = claimsIdentity.FindFirstValue(CurrentProjectName);
            var userName = User.Identity.Name;
            var content =
                await
                    _templateService.GetPullRequestTemplatesAsync(userName, repositoryName,
                        PathToPullRequestTemplate);

            return Ok(content);
        }

        [Route(RouteTemplatesConstants.IssueTemplate)]
        public async Task<IHttpActionResult> GetIssueTemplate()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var repositoryName = claimsIdentity.FindFirstValue(CurrentProjectName);
            var userName = User.Identity.Name;
            var content =
                await
                    _templateService.GetIssueTemplateAsync(userName, repositoryName,
                    PathToIssueTemplate);

            return Ok(content);
        }
    }
}