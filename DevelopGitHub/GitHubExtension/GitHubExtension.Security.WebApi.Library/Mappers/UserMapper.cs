﻿using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.StorageModels.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.WebApi.Library.Mappers
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
                Id = user.Id,
                Email = user.Email,
                GitHubId = user.ProviderId,
                UserName =  user.UserName
            };

            return userReturnModel;
        }
    }
}
