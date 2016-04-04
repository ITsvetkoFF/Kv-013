using System.Collections.Generic;
using Xunit;
using NSubstitute;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Mappers;
using GitHubExtension.Models.CommunicationModels;
using FluentAssertions;


namespace GitHubExtension.Security.Tests.TestExtensions
{
    public class UserToUserReturnModelTests
    {
        public static IEnumerable<object[]> DataForUserReturnModelTest
        {
            get
            {
                yield return new object[]
                {
                    new User
                    { 
                        Id="123", ProviderId=124, 
                        Email="Email@gamil.com",
                        UserName="Name"
                    },
                    new UserReturnModel
                    { 
                        Id="123", 
                        GitHubId=124, 
                        Email="Email@gamil.com", 
                        UserName="Name"}
                };
            }
        }

        [Theory]
        [MemberData("GetData")]
        public void UserReturnModelTest(User entityToUserReturnModelTest, UserReturnModel expectedUserReturnModel)
        {
            //Act
            UserReturnModel userReturnModel = entityToUserReturnModelTest.ToUserReturnModel();

            //Assert
            userReturnModel.ShouldBeEquivalentTo(expectedUserReturnModel);
        }
    }
}