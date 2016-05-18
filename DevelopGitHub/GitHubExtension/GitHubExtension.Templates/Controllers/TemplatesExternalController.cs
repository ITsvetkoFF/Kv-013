using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters;
using GitHubExtension.Infrastructure.Extensions.Identity;
using GitHubExtension.Templates.Commands;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.Constants;
using GitHubExtension.Templates.Mappers;
using GitHubExtension.Templates.Queries;

namespace GitHubExtension.Templates.Controllers
{
    public class TemplatesExternalController : ApiController
    {
        private const string PathToPullRequestTemplate = ".github/PULL_REQUEST_TEMPLATE.md";
        private const string PathToIssueTemplate = ".github/ISSUE_TEMPLATE.md";
        private readonly ITemplatesCommand _templatesCommand;
        private readonly ITemplatesQuery _templateQuery;

        public TemplatesExternalController(ITemplatesCommand templatesCommand, ITemplatesQuery templatesQuery)
        {
            _templatesCommand = templatesCommand;
            _templateQuery = templatesQuery;  
        }

        [HttpGet]
        [Route(RouteTemplatesConstants.PullRequestTemplate)]
        public async Task<IHttpActionResult> GetPullRequestTemplate()
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalAccessToken();
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
            var token = User.GetExternalAccessToken();
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

        [HttpPost]
        [Route(RouteTemplatesConstants.PullRequestTemplate)]
        [TemplateActivity(ActivityTypeName = ActivityTypeNames.CreatePullRequestTemplate)]
        public async Task<IHttpActionResult> CreatePullRequestTemplate([FromBody]CreateUpdateTemplateModel model)
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalAccessToken();

            CreateUpdateTemplateModel requestModel = 
                model.ToCreateModel(PathToPullRequestTemplate, repositoryName, token);

            var statusCode =
                await _templatesCommand.CreateTemplateAsync(requestModel);

            return StatusCode(statusCode);
        }

        [HttpPost]
        [Route(RouteTemplatesConstants.IssueTemplate)]
        [TemplateActivity(ActivityTypeName = ActivityTypeNames.CreateIssueRequestTemplate)]
        public async Task<IHttpActionResult> CreateIssueTemplate([FromBody]CreateUpdateTemplateModel model)
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalAccessToken();

            CreateUpdateTemplateModel requestModel =
                model.ToCreateModel(PathToIssueTemplate, repositoryName, token);

            var statusCode = await _templatesCommand.CreateTemplateAsync(requestModel);

            return StatusCode(statusCode);
        }

        [HttpPut]
        [Route(RouteTemplatesConstants.PullRequestTemplate)]
        [TemplateActivity(ActivityTypeName = ActivityTypeNames.UpdatePullRequestTemplate)]
        public async Task<IHttpActionResult> UpdatePullRequestTemplate([FromBody]CreateUpdateTemplateModel model)
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalAccessToken();
            var sha = await _templatesCommand.GetShaTemplate(repositoryName, PathToPullRequestTemplate, token);
            CreateUpdateTemplateModel requestModel = 
                model.ToUpdateModel(PathToPullRequestTemplate, repositoryName, token, sha);
           
            var statusCode = await _templatesCommand.UpdateTemplateAsync(requestModel);

            return StatusCode(statusCode);
        }

        [HttpPut]
        [Route(RouteTemplatesConstants.IssueTemplate)]
        [TemplateActivity(ActivityTypeName = ActivityTypeNames.UpdateIssueRequestTemplate)]
        public async Task<IHttpActionResult> UpdateIssueTemplate([FromBody]CreateUpdateTemplateModel model)
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalAccessToken();
            var sha = await _templatesCommand.GetShaTemplate(repositoryName, PathToIssueTemplate, token);
            CreateUpdateTemplateModel requestModel =
                model.ToUpdateModel(PathToIssueTemplate, repositoryName, token, sha);

            var statusCode = await _templatesCommand.UpdateTemplateAsync(requestModel);

            return StatusCode(statusCode);
        }
    }
}
