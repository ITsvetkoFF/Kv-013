using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Infrastructure.Extensions;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.Constants;
using GitHubExtension.Templates.ExtensionMethods;
using GitHubExtension.Templates.Mappers;
using GitHubExtension.Templates.Queries;

namespace GitHubExtension.Templates.Controllers
{
    [RoutePrefix(RouteTemplatesConstants.GetGitHubTemplatesRoute)]
    public class TemplatesQueryController : ApiController
    {
        private const string PathToPullRequestTemplate = ".github/PULL_REQUEST_TEMPLATE.md";
        private const string PathToIssueTemplate = ".github/ISSUE_TEMPLATE.md";
        private readonly ITemplatesQuery _templateQuery;

        public TemplatesQueryController(ITemplatesQuery templatesQuery)
        {
            _templateQuery = templatesQuery;  
        }
        
        [HttpGet]
        [Route(RouteTemplatesConstants.PullRequestTemplate)]
        public async Task<IHttpActionResult> GetPullRequestTemplate()
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalToken();
            var model = repositoryName.ToGetTemplateModel(PathToPullRequestTemplate, token);
            var content =
                await
                    _templateQuery.GetTemplatesAsync(model);

            if (content == null)
            {
                return NotFound();
            }

            return Ok(content);
        }

        [HttpGet]
        [Route(RouteTemplatesConstants.IssueTemplate)]
        public async Task<IHttpActionResult> GetIssueTemplate()
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalToken();
            var model = repositoryName.ToGetTemplateModel(PathToIssueTemplate, token);
            var content =
                await
                    _templateQuery.GetTemplatesAsync(model);

            if (content == null)
            {
                return NotFound();
            }

            return Ok(content);
        }

        [HttpGet]
        [Route(RouteTemplatesConstants.GetPullRequests)]
        public IHttpActionResult GetPullRequests()
        {
            var pullRequestTemplates = _templateQuery.GetPullRequests();

            return Ok(pullRequestTemplates);
        }

        [HttpGet]
        [Route(RouteTemplatesConstants.GetIssueTemplateCategories)]
        public IHttpActionResult GetIssueTemplateCategories()
        {
            var issueTemplatesCategories = _templateQuery.GetIssueTemplateCategories();

            return Ok(issueTemplatesCategories);
        }

        [HttpGet]
        [Route(RouteTemplatesConstants.GetIssueTemplateByCategoryId)]
        public IHttpActionResult GetIssueTemplateByCategoryId(int id)
        {
            var issueTemplates = _templateQuery.GetIssueTemplateByCategoryId(id);

            return Ok(issueTemplates);
        }
    }
}
