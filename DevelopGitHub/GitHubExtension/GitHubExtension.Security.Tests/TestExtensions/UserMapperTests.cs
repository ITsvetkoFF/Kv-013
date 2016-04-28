using System.Collections.Generic;

using FluentAssertions;

using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;

using Xunit;

namespace GitHubExtension.Security.Tests.TestExtensions
{
    public class UserMapperTests
    {
        public static IEnumerable<object[]> DataForUserReturnModelTest
        {
            get
            {
                yield return
                    new object[]
                    {
                        new User
                        {
                            ProviderId = 124,
                            Email = "Email@gamil.com",
                            UserName = "Name",
                            GitHubUrl = "https://api.github.com/users/userName"
                        },
                        new UserReturnModel
                        {
                            GitHubId = 124,
                            Email = "Email@gamil.com",
                            UserName = "Name",
                            GitHubUrl = "https://api.github.com/users/userName"
                        }
                    };
            }
        }

        [Theory]
        [MemberData("DataForUserReturnModelTest")]
        public void UserReturnModelTest(User entityToUserReturnModelTest, UserReturnModel expectedUserReturnModel)
        {
            // Act
            UserReturnModel userReturnModel = entityToUserReturnModelTest.ToUserReturnModel();

            // Assert
            userReturnModel.ShouldBeEquivalentTo(expectedUserReturnModel);
        }
    }
}