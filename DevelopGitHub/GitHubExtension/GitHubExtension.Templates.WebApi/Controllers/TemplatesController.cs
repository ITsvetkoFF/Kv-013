using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
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

            [Route(RouteConstants.PullRequestTemplate)]
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
                var content = await _templateService.GetPullRequestTemplatesAsync(userName, repositoryName, RouteConstants.PathToPullRequestTemplate);

                return Ok(content);
            }

            [Route(RouteConstants.IssueTemplate)]
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
                
                var content = await _templateService.GetIssueTemplateAsync(userName, repositoryName, RouteConstants.PathToIssueTemplate);

                return Ok(content);
            }
        }
}