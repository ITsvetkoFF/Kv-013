using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Activity.External.Tests.Extensions;
using GitHubExtension.Activity.External.WebAPI;
using GitHubExtension.Activity.External.WebAPI.Controllers;
using GitHubExtension.Activity.External.WebAPI.Models;
using GitHubExtension.Activity.External.WebAPI.Models.EventPayloads;
using GitHubExtension.Activity.External.WebAPI.Queries;
using GitHubExtension.Infrastructure.Constants;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Activity.External.Tests.TestsForControllers
{
    public class ActivityControllerGetActivityTests
    {
        public const string TestToken = "token";
        public const string TestRepository = "repository";

        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[]
                {
                    new EventsPaginationModel()
                    {
                        
                        AmountOfPages =  1,
                        Events = new List<GitHubEventModel>()
                        {
                            new GitHubEventModel()
                            {
                                Actor = new ActorModel()
                                {
                                    AvatarUrl = String.Empty,
                                    Id = 1,
                                    GravatarId = null,
                                    Login = "Someone",
                                    Url = null
                                },
                                Id = "1",
                                CreatedAt = DateTime.Now,
                                Type = GitHubEventTypeConstants.CreateEvent,
                                Payload = new CreateEventPayloadModel(),
                                Repo = new RepositoryShortModel()
                                {
                                    Id =  1,
                                    Name = "Repo",
                                    Url = string.Empty
                                }
                            }
                        }
                },
                    1
                };
            }
        }

        public static IEnumerable<object[]> DataForNotFoundResult
        {
            get
            {
                yield return new object[]
                {
                    null,
                    1
                };
            }
        }

        public static IEnumerable<object[]> DataForBadRequestResult
        {
            get
            {
                yield return new object[]
                {
                    null,
                    1
                };
            }
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public async Task OkResultTest(EventsPaginationModel events, int page)
        {
            // Arrange
            var query = Substitute.For<IGitHubEventsQuery>();
            query.GetGitHubEventsAsync(TestRepository, TestToken, page).Returns(Task.FromResult(events));
            var controller = new ActivityController(query);
            var claims = new[]
            {
                new Claim(ClaimConstants.ExternalAccessToken, TestToken),
                new Claim(ClaimConstants.CurrentProjectName, TestRepository),
            };
            controller.SetUserForController("user", claims);

            // Act
            IHttpActionResult result = await controller.GetGitHubActivity(page);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<EventsPaginationModel>>();
        }

        [Theory]
        [MemberData("DataForNotFoundResult")]
        public async Task NotFoundResult(EventsPaginationModel events, int page)
        {
            // Arrange
            var query = Substitute.For<IGitHubEventsQuery>();
            query.GetGitHubEventsAsync(TestRepository, TestToken, page).Returns(Task.FromResult(events));
            var controller = new ActivityController(query);
            var claims = new[]
            {
                new Claim(ClaimConstants.ExternalAccessToken, TestToken),
                new Claim(ClaimConstants.CurrentProjectName, TestRepository),
            };
            controller.SetUserForController("user", claims);

            // Act
            IHttpActionResult result = await controller.GetGitHubActivity(page);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [MemberData("DataForBadRequestResult")]
        public async Task BadResult(EventsPaginationModel events, int page)
        {
            // Arrange
            var query = Substitute.For<IGitHubEventsQuery>();
            query.GetGitHubEventsAsync(TestRepository, TestToken, page).Returns(Task.FromResult(events));
            var controller = new ActivityController(query);
            var claims = new[]
            {
                new Claim(ClaimConstants.ExternalAccessToken, TestToken),
            };
            controller.SetUserForController("user", claims);

            // Act
            IHttpActionResult result = await controller.GetGitHubActivity(page);

            // Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>(ActivityController.YouHaveNoRepositorySelected);
        }
    }
}
