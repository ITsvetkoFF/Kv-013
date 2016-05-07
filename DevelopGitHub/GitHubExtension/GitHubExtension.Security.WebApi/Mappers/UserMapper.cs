using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Models;

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
                GitHubUrl = model.Url
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

        public static CollaboratorWithUserIdModel ToCollaboratorWithUserId(this CollaboratorModel collaborator, string userId)
        {
            var collaboratorWithUserIdModel = new CollaboratorWithUserIdModel
            {
                UserId = userId,
                GitHubId = collaborator.Id,
                Login = collaborator.Login,
                Url = collaborator.Url
            };

            return collaboratorWithUserIdModel;
        }
    }
}