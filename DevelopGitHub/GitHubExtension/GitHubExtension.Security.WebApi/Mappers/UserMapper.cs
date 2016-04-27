using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtention.Preferences.WebApi;

namespace GitHubExtension.Security.WebApi.Mappers
{
    public static class UserMapper
    {
        public static User ToUserEntity(this GitHubUserModel model)
        {
            var user = new User()
            {
                Email = model.Email, 
                UserName = model.Login, 
                ProviderId = model.GitHubId, 
                GitHubUrl = model.Url,
                AvatarUrl = model.AvatarUrl.SaveAvatarToBlobStorage(model.Login)
            };

            return user;
        }

        public static UserReturnModel ToUserReturnModel(this User user)
        {
            UserReturnModel userReturnModel = new UserReturnModel()
            {
                Email = user.Email, 
                GitHubId = user.ProviderId, 
                UserName = user.UserName,
                GitHubUrl = user.GitHubUrl
            };

            return userReturnModel;
        }
    }
}