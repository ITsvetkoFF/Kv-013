using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.WebApi.Extensions.Cookie;
using GitHubExtension.Security.WebApi.Extensions.OwinContext;
using GitHubExtension.Security.WebApi.Extensions.SecurityContext;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Queries.Interfaces;

using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Controllers
{
    public class RepositoryController : BaseApiController
    {
        private readonly ApplicationUserManager _userManager;

        private readonly IGitHubQuery _gitHubQuery;

        private readonly ISecurityContextQuery _securityContextQuery;

        public RepositoryController(
            IGitHubQuery gitHubQuery, 
            ISecurityContextQuery securityContextQuery, 
            ApplicationUserManager userManager)
        {
            _gitHubQuery = gitHubQuery;
            _securityContextQuery = securityContextQuery;
            _userManager = userManager;
        }

        [HttpPatch]
        [AllowAnonymous]
        [Route(RouteConstants.AssignRolesToUser)]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] int repoId, [FromUri] int gitHubId, [FromBody] string roleToAssign)
        {
            User appUser = _userManager.FindByGitHubId(gitHubId);
            if (appUser == null)
            {
                return NotFound();
            }

            SecurityRole role = _securityContextQuery.GetUserRole(roleToAssign);
            if (role == null)
            {
                var error = string.Format(RoleValidationConstants.RoleNotExist, roleToAssign);
                return ModelError(RoleValidationConstants.Role, error);
            }

            bool updateRoleResult = await UpdateRole(appUser, role, repoId);
            if (!updateRoleResult)
            {
                return ModelError(RoleValidationConstants.Role, RoleValidationConstants.FailedToUpdateRole);
            }

            return Ok();
        }

        [HttpGet]
        [Route(RouteConstants.GetByIdRepository)]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var repository = await _securityContextQuery.GetRepositoryByIdAsync(id);
            if (repository == null)
            {
                return NotFound();
            }

            return Ok(repository.ToRepositoryViewModel());
        }

        [HttpGet]
        [Authorize]
        [Route(RouteConstants.GetCollaboratorsForRepository)]
        public async Task<IHttpActionResult> GetCollaboratorsForRepo(string repoName)
        {
            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");
            string userName = User.Identity.GetUserName();

            var gitHubCollaborators = await _gitHubQuery.GetCollaboratorsForRepo(userName, repoName, token);

            return Ok(gitHubCollaborators);
        }

        [HttpGet]
        [Authorize]
        [Route(RouteConstants.GetRepositoryForCurrentUser)]
        public IHttpActionResult GetRepositoryForCurrentUser()
        {
            string userId = User.Identity.GetUserId();

            var repos = _securityContextQuery.GetRepositoriesWhereUserHasRole(userId, RoleConstants.Admin);
            if (repos == null)
            {
                return BadRequest();
            }

            IEnumerable<RepositoryViewModel> repositoryViewModels = repos.Select(r => r.ToRepositoryViewModel());
            return Ok(repositoryViewModels);
        }

        [Authorize]
        [Route(RouteConstants.UpdateProject)]
        [HttpPatch]
        public async Task<IHttpActionResult> UpdateProject(Repository repository)
        {
            string currentUserId = User.Identity.GetUserId();
            User user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null)
            {
                return NotFound();
            }

            UserRepositoryRole userRepositoryRole = await _securityContextQuery.GetUserRoleOnRepositoryAsync(repository.Id, user.Id);
            if (userRepositoryRole == null)
            {
                var error = string.Format(RepositoryValidationConstants.UserDoesNotHaveRepository, user.Id, repository.Id);
                return ModelError(RepositoryValidationConstants.Repository, error);
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            Claim[] claimsToUpdate =
            {
                new Claim(ClaimConstants.CurrentProjectId, repository.Id.ToString()), 
                new Claim(ClaimConstants.CurrentProjectName, userRepositoryRole.Repository.FullName)
            };
            if (!UpdateClaims(claimsIdentity, claimsToUpdate, user.Id))
            {
                return ModelError(RepositoryValidationConstants.CurrentProject, RepositoryValidationConstants.CurrentProjectUpdateFailed);
            }

            AddClaimsToCookie(claimsToUpdate);
            ReSignInWithIdentity(claimsIdentity);
            return Ok();
        }

        private void ReSignInWithIdentity(ClaimsIdentity identity)
        {
            var authentication = GetRequestContext.Authentication();
            authentication.SignOut();
            authentication.SignIn(identity);
        }

        private async Task<bool> UpdateRole(User appUser, SecurityRole role, int repositoryId)
        {
            UserRepositoryRole existringRole = await _securityContextQuery.GetUserRoleOnRepositoryAsync(repositoryId, appUser.Id);
            if (existringRole != null)
            {
                appUser.UserRepositoryRoles.Remove(existringRole);
            }

            appUser.UserRepositoryRoles.Add(new UserRepositoryRole() { RepositoryId = repositoryId, SecurityRoleId = role.Id });
            IdentityResult updateResult = await _userManager.UpdateAsync(appUser);
            if (!updateResult.Succeeded)
            {
                return false;
            }

            var claimsIdentity = await appUser.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
            var claimsToUpdate = new[] { new Claim(role.Name, repositoryId.ToString()) };
            return UpdateClaims(claimsIdentity, claimsToUpdate, appUser.Id);
        }

        private bool UpdateClaims(ClaimsIdentity identity, Claim[] claimsToUpdate, string userId)
        {
            RemoveClaims(identity, claimsToUpdate, userId);
            var results = AddClaims(identity, claimsToUpdate, userId);
            return results.All(r => r.Succeeded);
        }

        private IdentityResult[] AddClaims(ClaimsIdentity claimsIdentity, Claim[] claimsToAdd, string userId)
        {
            IdentityResult[] results = new IdentityResult[claimsToAdd.Length];

            for (int i = 0; i < claimsToAdd.Length; i++)
            {
                results[i] = _userManager.AddClaim(userId, claimsToAdd[i]);
                claimsIdentity.AddClaim(claimsToAdd[i]);
            }

            return results;
        }

        private void RemoveClaims(ClaimsIdentity claimsIdentity, Claim[] claims, string userId)
        {
            foreach (var claim in claims)
            {
                var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == claim.Type);
                if (existingClaim != null)
                {
                    _userManager.RemoveClaim(userId, existingClaim);
                    claimsIdentity.RemoveClaim(existingClaim);
                }
            }
        }

        private void AddClaimsToCookie(Claim[] claims)
        {
            foreach (var claim in claims)
            {
                GetRequestContext.SetCookie(claim.Type, claim.Value);
            }
        }
    }
}