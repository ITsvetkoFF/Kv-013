using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GitHubExtension.Security.WebApi.Library.Controllers
{

    public class UserController:BaseApiController
    {
        [AllowAnonymous]
        //[Route("api/user")]///{gitHubId})]
        [Route("api/user")]
        [HttpPatch]
        public async Task<IHttpActionResult> UpdateProject()
        {
            string request = await this.Request.Content.ReadAsStringAsync();
            var repo = JsonConvert.DeserializeAnonymousType(request, new { repositoryId = "" });
            return Ok();
        }

    }
}
