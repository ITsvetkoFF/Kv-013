using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Extensions.SecurityContext;
using GitHubExtension.Security.WebApi.Queries.Interfaces;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Security.Tests.TestExtensions
{
    public class SecurityContextUserExtensionTests
    {
        private const string TestSearchNotExistUserName = "NotExistUserName";
        private const string TestCurrentUserName = "CurrentUserName";

        public static IEnumerable<object[]> DataForTests
        {
            get
            {
                yield return new object[]
                {
                    new List<User>()
                    {
                        new User
                        {
                            UserName = "TestUserName"
                        },
                        new User
                        {
                            UserName = "CurrentUserName"
                        },
                        new User
                        {
                            UserName = "NotCurrentUserName"
                        }
                    }
                };
            }
        }
        
        [Theory]
        [MemberData("DataForTests")]
        public void ShouldReturnEmptyListWhenUsersNotFound(IEnumerable<User> users)
        {
            // Arrange
            var securityContextQuery = MockForSecurityContextQuery(users);

            // Act
            var result = securityContextQuery.GetUsersByNameExceptCurrent(TestSearchNotExistUserName, TestCurrentUserName);
            
            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [MemberData("DataForTests")]
        public void ShouldReturnNotEmptyListWithoutCurrentUserWhenUsersFound(IEnumerable<User> users)
        {
            // Arrange
            var securityContextQuery = MockForSecurityContextQuery(users);

            // Act
            var result = securityContextQuery.GetUsersByNameExceptCurrent(TestCurrentUserName, TestCurrentUserName);

            // Assert
            result.Should()
                .NotBeEmpty()
                .And.NotContain(u => u.UserName == TestCurrentUserName)
                .And.Contain(u => u.UserName.Contains(TestCurrentUserName));
        }

        private static ISecurityContextQuery MockForSecurityContextQuery(IEnumerable<User> users)
        {
            var context = users.AsQueryable();
            var securityContextQuery = Substitute.For<ISecurityContextQuery>();
            securityContextQuery.Users.Returns(context);
            return securityContextQuery;
        }
    }
}
