using System.Linq;
using System.Web.Http;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Library.Mappers;
using GitHubExtension.Constant;

namespace GitHubExtension.Security.WebApi.Library.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    [RoutePrefix(RouteConstants.apiRoles)]
    public class RolesController : BaseApiController
    {
        public RolesController(ISecurityContext securityContext)
        {
            _securityContext = securityContext;
        }

        private readonly ISecurityContext _securityContext;
        
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public IHttpActionResult GetAllRoles()
        {
            var roles = _securityContext.SecurityRoles.AsEnumerable().Select(r => r.ToRoleViewModel());
            
            return Ok(roles);
        }
    }
}