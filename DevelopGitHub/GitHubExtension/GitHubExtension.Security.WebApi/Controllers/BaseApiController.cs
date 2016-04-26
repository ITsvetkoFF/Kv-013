using System.Web;
using System.Web.Http;
using System.Web.Routing;

using Microsoft.AspNet.Identity;

namespace GitHubExtension.Security.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        public RequestContext GetRequestContext
        {
            get
            {
                var context = new HttpContextWrapper(HttpContext.Current);
                var routeData = HttpContext.Current.Request.RequestContext.RouteData;

                var requestContext = new RequestContext(context, routeData);
                return requestContext;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("IdentityError", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        protected IHttpActionResult ModelError(string key, string errorMessage)
        {
            ModelState.AddModelError(key, errorMessage);
            return BadRequest(ModelState);
        }
    }
}