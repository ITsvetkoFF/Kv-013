using System.Collections.Generic;
using Xunit;
using NSubstitute;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Mappers;
using GitHubExtension.Models.CommunicationModels;
using FluentAssertions;


namespace GitHubExtension.Security.Tests.TestExtensions
{
    public class UserMapperTests
    {
        public static IEnumerable<object[]> GetData
        {
            get
            {
                yield return new object[]
                {
                    new User{ Id="123", ProviderId=124, Email="Email@gamil.com", UserName="Name"},
                    new UserReturnModel{ Id="123", GitHubId=124, Email="Email@gamil.com", UserName="Name"}
                };
            }
        }

        [Theory]
        [MemberData("GetData")]
        public void UserReturnModelTest(User EntityToUserReturnModelTest, UserReturnModel expectedUserReturnModel)
        {
            UserReturnModel userReturnModel = EntityToUserReturnModelTest.ToUserReturnModel();

            userReturnModel.ShouldBeEquivalentTo(expectedUserReturnModel);
        }
    }
}