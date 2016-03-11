using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private ApplicationUserManager _AppUserManager = null;
        private SecurityRoleManager _SecurityRoleManager = null;

        protected ApplicationUserManager ApplicationUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }


        protected SecurityRoleManager SecurityRoleManager
        {
            get
            {
                return _SecurityRoleManager ?? Request.GetOwinContext().GetUserManager<SecurityRoleManager>();
            }
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.ApplicationUserManager);
                }
                return _modelFactory;
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
                        ModelState.AddModelError("", error);
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
    }
}
