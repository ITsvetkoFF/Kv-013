using GitHubExtension.Security.DAL.Entities;
using GitHubExtension.Security.WebApi.Models;

namespace GithubExtension.Security.WebApi.Converters
{
    public static class UserConverter
    {
        public static User ToUserEntity(this GitHubUserModel model)
        {
            var user = new User()
            {
                Email = model.Email,
                UserName = model.Login,
                ProviderId = model.GitHubId,
                GitHubUrl = model.Url
            };

            return user;
        }
    }
}
