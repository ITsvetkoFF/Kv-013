using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Extensions.SecurityContext;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Queries.Interfaces;

namespace GitHubExtension.Security.WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly ISecurityContextQuery _securityContextQuery;

        public UserController(ISecurityContextQuery securityContextQuery)
        {
            _securityContextQuery = securityContextQuery;
        }

        [HttpGet]
        [Route(RouteConstants.SearchUsersByName)]
        public IHttpActionResult GetAllUsersByName(string userName)
        {
            var currentUserName = User.Identity.Name;
            IEnumerable<User> users = 
                _securityContextQuery.GetUsersByNameExceptCurrent(userName, currentUserName).ToList();
            if (!users.Any())
            {
                return NotFound();
            }

            IEnumerable<UserReturnModel> userReturnModels = users.Select(user => user.ToUserReturnModel());
            return Ok(userReturnModels);
        }
    }
}
