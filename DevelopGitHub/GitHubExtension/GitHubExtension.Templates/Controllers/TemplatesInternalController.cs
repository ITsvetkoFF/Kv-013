using System.Linq;
using System.Web.Http;
using GitHubExtension.Templates.Constants;
using GitHubExtension.Templates.Queries;

namespace GitHubExtension.Templates.Controllers
{
    public class TemplatesInternalController : ApiController
    {
         private readonly ITemplatesQuery _templateQuery;

         public TemplatesInternalController(ITemplatesQuery templatesQuery)
        {
            _templateQuery = templatesQuery;  
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
