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
using Microsoft.WindowsAzure.Storage.Blob;
using GitHubExtention.Preferences.WebApi.Queries;


namespace GitHubExtention.Preferences.WebApi.Controllers
{
    [RoutePrefix(RouteConstants.ApiUser)]
    public class UserPreferencesController : ApiController
    {      
        private readonly ApplicationUserManager _userManager;

        private readonly IAzureContainerQuery _blobContainer;

        public UserPreferencesController(ApplicationUserManager userManager, IAzureContainerQuery blobContainer)
        {
            _userManager = userManager;
            _blobContainer = blobContainer;
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
            User user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null)
                return NotFound();

            var multipartStreamProvider = new AzureBlobStorageMultipartProvider(_blobContainer, User.Identity.Name);
            
            await Request.Content.ReadAsMultipartAsync<AzureBlobStorageMultipartProvider>(multipartStreamProvider);
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
