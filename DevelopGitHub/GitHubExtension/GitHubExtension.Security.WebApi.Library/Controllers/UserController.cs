using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using GitHubExtension.Security.DAL.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
using System.Security.Claims;


namespace GitHubExtension.Security.WebApi.Library.Controllers
{

    public class UserController:BaseApiController
    {
        private readonly ISecurityContext _securityContext;
        private readonly ApplicationUserManager _userManager;


        public UserController(
            ISecurityContext securityContext, 
            ApplicationUserManager userManager)
        {
            _securityContext = securityContext; 
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Route("api/user")]
        [HttpPatch]
        public async Task<IHttpActionResult> UpdateProject()
        {
            string request = await this.Request.Content.ReadAsStringAsync();
            var repo = JsonConvert.DeserializeAnonymousType(request, new { repositoryId = "" });
            int repositoryId;

            if(!Int32.TryParse(repo.repositoryId, out repositoryId))
            {
                ModelState.AddModelError("repo", string.Format("repository with id '{0}' does not exists in the system", repo.repositoryId));
                return BadRequest(ModelState);
            }

            
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == User.Identity.GetUserId());
            if (user == null)
                return NotFound();
            var repository = _securityContext.UserRepository.FirstOrDefault(r => r.RepositoryId == repositoryId && r.UserId == user.Id.ToString());
            if(repository == null)
            {
                ModelState.AddModelError("repo", string.Format("user with id '{0}' does not have a repository with id '{1}'",user.Id, repo.repositoryId));
                return BadRequest(ModelState);
            }

            var claimsIdentity = await user.GenerateUserIdentityAsync(_userManager, DefaultAuthenticationTypes.ApplicationCookie);
            var existingClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Value == repositoryId.ToString());
            if (existingClaim != null)
                _userManager.RemoveClaim(user.Id, existingClaim);
            
            var addClaimResult = await _userManager.AddClaimAsync(user.Id, new Claim("CurrentProject", repositoryId.ToString()));

            if (!addClaimResult.Succeeded)
            {
                ModelState.AddModelError("CurrentpProject", "Failed to update current project");
                return BadRequest(ModelState);
            }

            return Ok();
        }

    }
}
