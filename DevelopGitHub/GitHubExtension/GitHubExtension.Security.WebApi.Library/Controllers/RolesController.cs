using System.Linq;
using System.Web;
using System.Web.Http;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Converters;

namespace GitHubExtension.Security.WebApi.Library.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseApiController
    {
        public RolesController(ISecurityContext securityContext)
        {
            _securityContext = securityContext;
        }

        private readonly ISecurityContext _securityContext;
        

        [Route("")]
        [AllowAnonymous]
        public IHttpActionResult GetAllRoles()
        {
            var roles = _securityContext.SecurityRoles.AsEnumerable().Select(r => r.ToRoleViewModel());
            
            return Ok(roles);
        }
    }
}