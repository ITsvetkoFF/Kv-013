using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Library.Services;
using Newtonsoft.Json;

namespace GitHubExtension.Security.WebApi.Library.Controllers
{
    [RoutePrefix("api/templates")]
     public class TemplatesController : BaseApiController
    {
        #region private fields

        private readonly ITemplateService _templateService;

        #endregion
        
        public TemplatesController(ITemplateService templateService)
        {
             _templateService = templateService;
        }

        [Route("pullRequestTemplate")]
        public async Task<string> GetPullRequestTemplate()
        {
            var userName = User.Identity.Name;
            var repositoryName = "ForSoftTheme";
            var pathToFile = "readme";
            var content = await _templateService.GetPullRequestTemplatesAsync(userName, repositoryName,pathToFile);
             
            return content;
        }

        [Route("issueTemplate")]
        public async Task<String> GetIssueTemplate()
        {
            var userName = User.Identity.Name;
            var repositoryName = "ForSoftTheme";
            var pathToFile = "readme";
            var content = await _templateService.GetIssueTemplateAsync(userName, repositoryName, pathToFile);

            return content;
        }
    }
}
