using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Templates.WebApi.Constants;
using GitHubExtension.Templates.WebApi.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Templates.WebApi.Controllers
{
    [RoutePrefix(RouteConstants.GetGitHubTemplatesRoute)]
    public class TemplatesController : BaseApiController
    {
        #region private fields

        private readonly ITemplateService _templateService;
        private const string CurrentProjectName = "CurrentProjectName";

        #endregion

        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [Route(RouteConstants.PullRequestTemplate)]
        public async Task<IHttpActionResult> GetPullRequestTemplate()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var repositoryName = claimsIdentity.FindFirstValue(CurrentProjectName);
            var userName = User.Identity.Name;
            var content =
                await
                    _templateService.GetPullRequestTemplatesAsync(userName, repositoryName,
                        RouteConstants.PathToPullRequestTemplate);

            return Ok(content);
        }

        [Route(RouteConstants.IssueTemplate)]
        public async Task<IHttpActionResult> GetIssueTemplate()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var repositoryName = claimsIdentity.FindFirstValue(CurrentProjectName);
            var userName = User.Identity.Name;
            var content =
                await
                    _templateService.GetIssueTemplateAsync(userName, repositoryName, 
                    RouteConstants.PathToIssueTemplate);

            return Ok(content);
        }
    }
}