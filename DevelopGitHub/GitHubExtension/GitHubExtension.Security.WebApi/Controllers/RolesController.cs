using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GitHubExtension.Constant;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    [RoutePrefix(RouteConstants.ApiRoles)]
    public class RolesController : BaseApiController
    {
        private readonly ISecurityContext securityContext;

        public RolesController(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public IHttpActionResult GetAllRoles()
        {
            IEnumerable<RoleViewModel> roles = this.securityContext.SecurityRoles.AsEnumerable().Select(r => r.ToRoleViewModel());

            return Ok(roles);
        }
    }
}