using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Templates.WebApi.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Templates.WebApi.Controllers
{
        [RoutePrefix("api/templates")]
        public class TemplatesController : BaseApiController
        {
            #region private fields

            private readonly ITemplateService _templateService;
            private readonly ApplicationUserManager _userManager;
            private readonly ISecurityContext _securityContext;

            #endregion

            public TemplatesController(ITemplateService templateService, ApplicationUserManager userManager,
                ISecurityContext securityContext)
            {
                _templateService = templateService;
                _userManager = userManager;
                _securityContext = securityContext;
            }

            [Route("pullRequestTemplate")]
            public async Task<IHttpActionResult> GetPullRequestTemplate()
            {
                var currentUserId = User.Identity.GetUserId();
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
                var claimsIdentity =
                    await user.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
                var currentRepoId =
                    Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "CurrentProjectId").Value);

                var repositoryName = _securityContext.Repositories.FirstOrDefault(x => x.Id == currentRepoId).Name;
                var userName = User.Identity.Name;
                var pathToFile = ".github/PULL_REQUEST_TEMPLATE.md";
                var content = await _templateService.GetPullRequestTemplatesAsync(userName, repositoryName, pathToFile);

                return Ok(content);
            }

            [Route("issueTemplate")]
            public async Task<IHttpActionResult> GetIssueTemplate()
            {
                var currentUserId = User.Identity.GetUserId();
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
                var claimsIdentity =
                    await user.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
                var currentRepoId =
                    Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "CurrentProjectId").Value);

                var repositoryName = _securityContext.Repositories.FirstOrDefault(x => x.Id == currentRepoId).Name;
                var userName = User.Identity.Name;
                var pathToFile = ".github/ISSUE_TEMPLATE.md";
                var content = await _templateService.GetIssueTemplateAsync(userName, repositoryName, pathToFile);

                return Ok(content);
            }
        }
}