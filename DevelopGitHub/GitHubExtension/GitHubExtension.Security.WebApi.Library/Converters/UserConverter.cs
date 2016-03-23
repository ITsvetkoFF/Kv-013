using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.StorageModels.Identity;

namespace GitHubExtension.Security.WebApi.Converters
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
