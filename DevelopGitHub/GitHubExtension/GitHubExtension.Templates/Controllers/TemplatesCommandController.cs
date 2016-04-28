using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Templates.Commands;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.Constants;
using GitHubExtension.Templates.ExtensionMethods;
using GitHubExtension.Templates.Mappers;

namespace GitHubExtension.Templates.Controllers
{
    [RoutePrefix(RouteTemplatesConstants.GetGitHubTemplatesRoute)]
    public class TemplatesCommandController : ApiController
    {
        private const string PathToPullRequestTemplate = ".github/PULL_REQUEST_TEMPLATE.md";
        private const string PathToIssueTemplate = ".github/ISSUE_TEMPLATE.md";
        private readonly ITemplatesCommand _templatesCommand;

        public TemplatesCommandController(ITemplatesCommand templatesCommand)
        {
            _templatesCommand = templatesCommand;
        }

        [HttpPost]
        [Route(RouteTemplatesConstants.AddUpdateRequestTemplate)]
        public async Task<IHttpActionResult> CreatePullRequestTemplate([FromBody]CreateUpdateTemplateModel model)
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalToken();

            CreateUpdateTemplateModel requestModel = 
                model.ToCreateModel(PathToPullRequestTemplate, repositoryName, token);

            var statusCode =
                await _templatesCommand.CreateTemplateAsync(requestModel);

            return StatusCode(statusCode);
        }

        [HttpPost]
        [Route(RouteTemplatesConstants.AddUpdateIssueTemplate)]
        public async Task<IHttpActionResult> CreateIssueTemplate([FromBody]CreateUpdateTemplateModel model)
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalToken();

            CreateUpdateTemplateModel requestModel =
                model.ToCreateModel(PathToIssueTemplate, repositoryName, token);

            var statusCode = await _templatesCommand.CreateTemplateAsync(requestModel);

            return StatusCode(statusCode);
        }

        [Authorize]
        [HttpPut]
        [Route(RouteTemplatesConstants.AddUpdateRequestTemplate)]
        public async Task<IHttpActionResult> UpdatePullRequestTemplate([FromBody]CreateUpdateTemplateModel model)
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalToken();
            var sha = await _templatesCommand.GetShaTemplate(repositoryName, PathToPullRequestTemplate, token);
            CreateUpdateTemplateModel requestModel = 
                model.ToUpdateModel(PathToPullRequestTemplate, repositoryName, token, sha);
           
            var statusCode = await _templatesCommand.UpdateTemplateAsync(requestModel);

            return StatusCode(statusCode);
        }

        [HttpPut]
        [Route(RouteTemplatesConstants.AddUpdateIssueTemplate)]
        public async Task<IHttpActionResult> UpdateIssueTemplate([FromBody]CreateUpdateTemplateModel model)
        {
            var repositoryName = User.GetCurrentProjectName();
            var token = User.GetExternalToken();
            var sha = await _templatesCommand.GetShaTemplate(repositoryName, PathToIssueTemplate, token);
            CreateUpdateTemplateModel requestModel =
                model.ToUpdateModel(PathToIssueTemplate, repositoryName, token, sha);

            var statusCode = await _templatesCommand.UpdateTemplateAsync(requestModel);

            return StatusCode(statusCode);
        }
    }
}