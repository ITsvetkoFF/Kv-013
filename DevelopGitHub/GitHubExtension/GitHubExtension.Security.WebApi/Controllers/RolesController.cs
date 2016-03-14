using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GithubExtension.Security.DAL.Context;
using GitHubExtension.Security.WebApi.Converters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GitHubExtension.Security.WebApi.Models;
using Microsoft.AspNet.Identity.Owin;

namespace GitHubExtension.Security.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseApiController
    {
        private SecurityContext Context
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<SecurityContext>();
            }
        }

        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string Id)
        {
            var role = await SecurityRoleManager.FindByIdAsync(Id);

            if (role != null)
            {
                return Ok(TheModelFactory.Create(role));
            }

            return NotFound();

        }

        [Route("")]
        [AllowAnonymous]
        public IHttpActionResult GetAllRoles()
        {
            var roles = Context.SecurityRoles.AsEnumerable().Select(r => r.ToRoleViewModel());
            
            return Ok(roles);
        }

        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateRoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new IdentityRole { Name = model.Name };

            var result = await this.SecurityRoleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            Uri locationHeader = new Uri(Url.Link("GetRoleById", new { id = role.Id }));

            return Created(locationHeader, TheModelFactory.Create(role));

        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteRole(string Id)
        {

            var role = await this.SecurityRoleManager.FindByIdAsync(Id);

            if (role != null)
            {
                IdentityResult result = await this.SecurityRoleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();
        }

        [Route("ManageUsersInRole")]
        public async Task<IHttpActionResult> ManageUsersInRole(UsersInRoleModel model)
        {
            var role = await this.SecurityRoleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ModelState.AddModelError("", "SecurityRole does not exist");
                return BadRequest(ModelState);
            }

            foreach (string user in model.EnrolledUsers)
            {
                var appUser = await this.ApplicationUserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", String.Format("User: {0} does not exists", user));
                    continue;
                }

                if (!this.ApplicationUserManager.IsInRole(user, role.Name))
                {
                    IdentityResult result = await this.ApplicationUserManager.AddToRoleAsync(user, role.Name);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", String.Format("User: {0} could not be added to role", user));
                    }

                }
            }

            foreach (string user in model.RemovedUsers)
            {
                var appUser = await this.ApplicationUserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", String.Format("User: {0} does not exists", user));
                    continue;
                }

                IdentityResult result = await this.ApplicationUserManager.RemoveFromRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", String.Format("User: {0} could not be removed from role", user));
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}