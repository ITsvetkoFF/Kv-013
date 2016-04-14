﻿using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Constant;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Controllers
{
    public class RepositoryController : BaseApiController
    {
        private readonly IGithubService gitHubService;
        private readonly ISecurityContext securityContext;
        private readonly ApplicationUserManager userManager;

        public RepositoryController(
            IGithubService gitHubService,
            ISecurityContext securityContext,
            ApplicationUserManager userManager)
        {
            this.gitHubService = gitHubService;
            this.securityContext = securityContext;
            this.userManager = userManager;
        }

        // id is a GitHub id of a repo
        [HttpGet]
        [Route(RouteConstants.GetByIdRepository)]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var repository = await this.securityContext.Repositories.FirstOrDefaultAsync(r => r.GitHubId == id);
            if (repository == null)
            {
                return NotFound();
            }

            return Ok(repository.ToRepositoryViewModel());
        }

        [HttpGet]
        [Authorize]
        [Route(RouteConstants.GetRepositoryForCurrentUser)]
        public async Task<IHttpActionResult> GetRepositoryForCurrentUser()
        {
            string userId = User.Identity.GetUserId();
            var role = await this.securityContext.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
            if (role == null)
            {
                return NotFound();
            }

            var repos = this.securityContext.Repositories
                .Where(r => r.UserRepositoryRoles.Any(ur => ur.UserId == userId && ur.SecurityRoleId == role.Id))
                .AsEnumerable().Select(r => r.ToRepositoryViewModel());
            return Ok(repos);
        }

        [HttpGet]
        [Authorize]
        [Route(RouteConstants.GetCollaboratorsForRepository)]
        public async Task<IHttpActionResult> GetCollaboratorsForRepo(string repoName)
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            var gitHubCollaborators = await this.gitHubService.GetCollaboratorsForRepo(userName, repoName, token);

            return Ok(gitHubCollaborators);
        }

        [Authorize]
        [Route("api/repos/current")]
        [HttpPatch]
        public async Task<IHttpActionResult> UpdateProject(Repository repo)
        {
            string currentUserId = User.Identity.GetUserId();
            User user = await this.userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
            if (user == null)
            {
                return NotFound();
            }

            var repository = this.securityContext.UserRepository.FirstOrDefault(r => r.RepositoryId == repo.Id && r.UserId == user.Id);
            if (repository == null)
            {
                ModelState.AddModelError(
                    "repo", 
                    string.Format("user with id '{0}' does not have a repository with id '{1}'", user.Id, repo.Id));
                return BadRequest(ModelState);
            }

            var claimsIdentity = await user.GenerateUserIdentityAsync(this.userManager, DefaultAuthenticationTypes.ApplicationCookie);
            var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "CurrentProjectId");
            if (existingClaim != null)
            {
                this.userManager.RemoveClaim(user.Id, existingClaim);
            }

            var addClaimResult = await this.userManager.AddClaimAsync(user.Id, new Claim("CurrentProjectId", repo.Id.ToString()));

            if (!addClaimResult.Succeeded)
            {
                ModelState.AddModelError("CurrentpProject", "Failed to update current project");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}