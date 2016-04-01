﻿using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.WebApi.Library.Converters;
using Microsoft.AspNet.Identity.Owin;


namespace GitHubExtension.Security.WebApi.Library.Controllers
{
    public class BaseApiController : ApiController
    {
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
    }
}
