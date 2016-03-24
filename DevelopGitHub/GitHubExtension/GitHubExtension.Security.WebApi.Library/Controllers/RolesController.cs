using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Converters;

namespace GitHubExtension.Security.WebApi.Library.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
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