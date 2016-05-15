using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Preferences.DAL.Model;
using GitHubExtention.Preferences.WebApi.Extensions;
using GitHubExtention.Preferences.WebApi.Models;
using GitHubExtention.Preferences.WebApi.Queries;
using Microsoft.AspNet.Identity;

namespace GitHubExtention.Preferences.WebApi.Controllers
{
    [RoutePrefix(RouteConstants.ApiUser)]
    public class UserPreferencesController : ApiController
    {
        private readonly IPreferencesQuery _query;

        private readonly IAzureContainerQuery _container;

        public UserPreferencesController(IPreferencesQuery query, IAzureContainerQuery container)
        {
            _query = query;
            _container = container;
        }

        [HttpPost]
        [Route(RouteConstants.Avatar)]
        public async Task<IHttpActionResult> Post(FileModel file)
        {
            User user = _query.Users.GetUser(User.Identity.GetUserId());
            if (user == null)
            {
                return NotFound(); 
            }

            if (file.IsValidImageFormat())
            {
                string newFileUrl = await _container.UpdateBlob(file);

                if (!newFileUrl.Equals(user.AvatarUrl))
                {
                    _container.DeleteBlob(user.AvatarUrl);
                    user.AvatarUrl = newFileUrl;
                    _query.SaveChanges();
                }
            }

            return Ok<string>(user.AvatarUrl);
        }

        [HttpGet]
        [Route(RouteConstants.Avatar)]
        public async Task<IHttpActionResult> Get()
        {
            User user = _query.Users.GetUser(User.Identity.GetUserId());
            if (user == null)
            {
                return NotFound();
            }

            return Ok<string>(user.AvatarUrl);
        }
    }
}
