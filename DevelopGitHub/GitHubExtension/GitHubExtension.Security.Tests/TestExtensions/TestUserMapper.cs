using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Mappers;
using GitHubExtension.Models.CommunicationModels;


namespace GitHubExtension.Security.Tests.TestExtensions
{
    public class TestUserMapper
    {
        [Fact]
        public void TestToUserReturnModel()
        {
            User user = new User
            {
                Id = "123",
                ProviderId = 124,
                GitHubUrl = "GithubUrl",
                UserName="UserName",
            };

            UserReturnModel expectedUserReturnModel = new UserReturnModel
            {
                Id =user.Id,
                Email = user.Email,
                GitHubId = user.ProviderId,
                UserName =  user.UserName
            };
            UserReturnModelComparer comparer =new UserReturnModelComparer();

            UserReturnModel userReturnModel = user.ToUserReturnModel();

            Assert.Equal<UserReturnModel>(expectedUserReturnModel, userReturnModel, comparer);
        }
    }

    public class UserReturnModelComparer : IEqualityComparer<UserReturnModel>
    {

        public bool Equals(UserReturnModel x, UserReturnModel y)
        {
            if (x.Id != y.Id) return false;
            if (x.Email != y.Email) return false;
            if (x.GitHubId != y.GitHubId) return false;
            if (x.UserName != y.UserName) return false;
            return true;
        }

        public int GetHashCode(UserReturnModel obj)
        {
            return base.GetHashCode();
        }
    }
}
