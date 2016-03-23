using System.Linq;
using System.Web;
using System.Web.Http;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.WebApi.Converters;
using Microsoft.AspNet.Identity.Owin;

namespace GitHubExtension.Security.WebApi.Library.Controllers
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

        [Route("")]
        [AllowAnonymous]
        public IHttpActionResult GetAllRoles()
        {
            var roles = Context.SecurityRoles.AsEnumerable().Select(r => r.ToRoleViewModel());
            
            return Ok(roles);
        }
    }
}