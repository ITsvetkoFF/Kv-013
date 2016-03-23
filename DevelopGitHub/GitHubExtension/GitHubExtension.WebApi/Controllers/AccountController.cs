using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using GitHubExtension.Domain.Interfaces;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Converters;
using GitHubExtension.Security.WebApi.Library.Controllers;
using GitHubExtension.Security.WebApi.Library.Converters;
using GitHubExtension.Security.WebApi.Library.Exceptions;
using GitHubExtension.Security.WebApi.Library.Results;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;


namespace GitHubExtension.Security.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Account1")]
    public class AccountsController: BaseApiController
    {
        #region private fields
        private readonly IGithubService _githubService;
        private readonly ISecurityContext _securityContext;
        private readonly IAuthService _authService;
        #endregion

        public AccountsController(
           )
        {
            //_githubService = githubService;
            //_securityContext = securityContext;
            //_authService = authService;
        }
       
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)// The first call, after click login with GitHub, and call when we write a info user
        {
            // if not allready authenticated sending user to GitHub
            if (!User.Identity.IsAuthenticated)
                return new ChallengeResult(provider, this);

            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalLoginToken");

            //TODO: Log
            if (token == null)
                throw new TokenNotFoundException();

            GitHubUserModel userReadModel = await _githubService.GetUserAsync(token);

            var user = await _authService.FindAsync(new UserLoginInfo("GitHub", userReadModel.GitHubId.ToString()));

            if (user == null)
            {
                #region move to service
                SecurityRole role = await _securityContext.SecurityRoles.FirstOrDefaultAsync(r => r.Name == "Admin");
                if (role == null)
                    return InternalServerError();

                GitHubUserModel gitHubUserModel = await _githubService.GetUserAsync(token);
                List<RepositoryDto> repositories = await _githubService.GetReposAsync(token);

                ICollection<UserRepositoryRole> rolesForInitialRepositories = repositories
                    .Select(r => new UserRepositoryRole() { Repository = r.ToEntity(), SecurityRole = role })
                        .ToList();

                gitHubUserModel.ToUserEntity().UserRepositoryRoles = rolesForInitialRepositories;

                IdentityResult addUserResult = await ApplicationUserManager.CreateAsync(user);
                if (!addUserResult.Succeeded)
                    return GetErrorResult(addUserResult);

                if (repositories.Any(r => !ApplicationUserManager
                        .AddClaim(gitHubUserModel.ToUserEntity().Id,
                            new Claim(role.Name, r.GitHubId.ToString())).Succeeded))
                    return BadRequest();

                #endregion

                return Ok(gitHubUserModel);
            }
            return BadRequest();
        }
    }
}