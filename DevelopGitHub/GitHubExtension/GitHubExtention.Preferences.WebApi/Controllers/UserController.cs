using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtention.Preferences.WebApi.Constants;


namespace GitHubExtention.Preferences.WebApi.Controllers
{
    [RoutePrefix(RouteConstants.ApiUser)]
    public class UserController : ApiController
    {      
        private readonly ApplicationUserManager _userManager;

        public UserController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route(RouteConstants.Avatar)]
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent(AvatarConstants.FormData))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string currentUserId = User.Identity.GetUserId();
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
            if (user == null)
                return NotFound();

            var multipartStreamProvider = new AzureBlobStorageMultipartProvider(BlobHelper.GetWebApiContainer(), User.Identity.Name);
            var readDate = await Request.Content.ReadAsMultipartAsync<AzureBlobStorageMultipartProvider>(multipartStreamProvider);
            if ((!string.IsNullOrEmpty(multipartStreamProvider._fileLocation) && !user.AvatarUrl.Equals(multipartStreamProvider._fileLocation)))
            {
                multipartStreamProvider.DeleteBlob(user.AvatarUrl);
                user.AvatarUrl = multipartStreamProvider._fileLocation;
                IdentityResult updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    ModelState.AddModelError(AvatarConstants.ErrorModel, AvatarConstants.ErrorMessage);
                    return BadRequest(ModelState);
                }
            }

            return Ok<string>(user.AvatarUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(RouteConstants.Avatar)]
        public async Task<IHttpActionResult> Get()
        {
            string currentUserId = User.Identity.GetUserId();
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
            if (user == null)
                return NotFound();
            return Ok<string>(user.AvatarUrl);
        }
    }
}
