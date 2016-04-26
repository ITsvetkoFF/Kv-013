using System.Linq;
using System.Web.Http;

using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Mappers;

namespace GitHubExtension.Security.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseApiController
    {
        private readonly ISecurityContext _securityContext;

        public RolesController(ISecurityContext securityContext)
        {
            _securityContext = securityContext;
        }

        [Route("")]
        [AllowAnonymous]
        public IHttpActionResult GetAllRoles()
        {
            var roles = _securityContext.SecurityRoles.AsEnumerable().Select(r => r.ToRoleViewModel());

            return Ok(roles);
        }
    }
}