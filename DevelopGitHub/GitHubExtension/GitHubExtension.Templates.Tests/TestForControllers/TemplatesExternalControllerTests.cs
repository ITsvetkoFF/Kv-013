using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Templates.Commands;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.Controllers;
using GitHubExtension.Templates.Queries;
using GitHubExtension.Templates.Tests.Extensions;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Templates.Tests.TestForControllers
{
    public class TemplatesExternalControllerTests
    {
        private const string PathToPullRequestTemplate = ".github/PULL_REQUEST_TEMPLATE.md";
        private const string PathToIssueTemplate = ".github/ISSUE_TEMPLATE.md";
        private const string CurrentProjectNameClaim = "CurrentProjectName";
        private const string ExternalAccessTokenClaim = "ExternalAccessToken";
        private const string CurrentProjectNameValue = "alexrrrr/TestTemplates";
        private const string ExternalAccessTokenValue = "d9469b1e4b8b5df5655cfd410c5e52a45951faad";
        private const string TestUserName = "Test";
        private const string Content = "content";
        private const string ContentNull = null;
        private const string Message = "message";
        private const string MessageNull = null;
        private const string Sha = "sha";
        private const string ShaNull = null;

        public static IEnumerable<object[]> DataForIssueNotFoundResult
        {
            get
            {
                yield return new object[]
                {
                        new GetTemplateModel()
                        {
                        PathToFile = PathToIssueTemplate
                        },
                        ContentNull
                };
            }
        }

        public static IEnumerable<object[]> DataForGetIssueOkResult
        {
            get
            {
                yield return new object[]
                {
                    new GetTemplateModel()
                    {
                        PathToFile = PathToIssueTemplate
                    },
                    Content
                };
            }
        }

        public static IEnumerable<object[]> DataForGetPullRuqestNotFoundResult
        {
            get
            {
                yield return new object[]
                {
                        new GetTemplateModel()
                        {
                        PathToFile = PathToPullRequestTemplate
                        },
                        ContentNull
                };
            }
        }

        public static IEnumerable<object[]> DataForGetPullRequestOkResult
        {
            get
            {
                yield return new object[]
                {
                    new GetTemplateModel()
                    {
                        PathToFile = PathToPullRequestTemplate
                    },
                    Content
                };
            }
        }

        public static IEnumerable<object[]> DataForGetIssueUnprocessableEntityResult
        {
            get
            {
                yield return new object[]
                {
                        new CreateUpdateTemplateModel()
                        {
                            Message = MessageNull,
                            Content = ContentNull,
                            Path = PathToIssueTemplate
                        },
                };
            }
        }

        public static IEnumerable<object[]> DataForIssueCreatedResult
        {
            get
            {
                yield return new object[]
                {
                     new CreateUpdateTemplateModel()
                        {
                            Message = Message,
                            Content = Content,
                            Path = PathToIssueTemplate
                        },
                };
            }
        }

        public static IEnumerable<object[]> DataForPullRequestCreatedResult
        {
            get
            {
                yield return new object[]
                {
                     new CreateUpdateTemplateModel()
                        {
                            Message = Message,
                            Content = Content,
                            Path = PathToPullRequestTemplate
                        },
                };
            }
        }

        public static IEnumerable<object[]> DataForIssueUnprocessableEntityResult
        {
            get
            {
                yield return new object[]
                {
                        new CreateUpdateTemplateModel()
                        {
                            Message = MessageNull,
                            Content = ContentNull,
                            Sha = ShaNull,
                            Path = PathToIssueTemplate
                        },
                };
            }
        }

        public static IEnumerable<object[]> DataForIssueOkResult
        {
            get
            {
                yield return new object[]
                {
                     new CreateUpdateTemplateModel()
                        {
                            Message = Message,
                            Content = Content,
                            Sha = Sha,
                            Path = PathToIssueTemplate
                        },
                };
            }
        }

        public static IEnumerable<object[]> DataForPullRequestUnprocessableEntityResult
        {
            get
            {
                yield return new object[]
                {
                        new CreateUpdateTemplateModel()
                        {
                            Message = MessageNull,
                            Content = ContentNull,
                            Sha = ShaNull,
                            Path = PathToPullRequestTemplate
                        },
                };
            }
        }

        public static IEnumerable<object[]> DataForPullRequestOkResult
        {
            get
            {
                yield return new object[]
                {
                     new CreateUpdateTemplateModel()
                        {
                            Message = Message,
                            Content = Content,
                            Sha = Sha,
                            Path = PathToPullRequestTemplate
                        },
                };
            }
        }

        public Claim[] Claims
        {
            get
            {
                return new[]
                {
                    new Claim(CurrentProjectNameClaim, CurrentProjectNameValue),
                    new Claim(ExternalAccessTokenClaim, ExternalAccessTokenValue)
                };
            }
        }

        [Theory]
        [MemberData("DataForGetIssueOkResult")]
        public async void ShoudReturnOkForGetIssueTemplate(GetTemplateModel model, string content)
        {
            // Arrange
            var templatesQuery = Substitute.For<ITemplatesQuery>();
            templatesQuery.GetTemplatesAsync(model).ReturnsForAnyArgs(Task.FromResult(content));
            TemplatesExternalController controller = new TemplatesExternalController(Substitute.For<ITemplatesCommand>(), templatesQuery);
            controller.SetUserForController(TestUserName, Claims);

            // Act
            IHttpActionResult result = await controller.GetIssueTemplate();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Theory]
        [MemberData("DataForIssueNotFoundResult")]
        public async void ShoudReturnNotFoundForGetIssueTemplate(GetTemplateModel model, string content)
        {
            // Arrange
            var templatesQuery = Substitute.For<ITemplatesQuery>();
            templatesQuery.GetTemplatesAsync(model).ReturnsForAnyArgs(content);
            TemplatesExternalController controller = new TemplatesExternalController(Substitute.For<ITemplatesCommand>(), templatesQuery);
            controller.SetUserForController(TestUserName, Claims);

            // Act
            IHttpActionResult result = await controller.GetIssueTemplate();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [MemberData("DataForGetPullRequestOkResult")]
        public async void ShoudReturnOkForGetPullRequestTemplate(GetTemplateModel model, string content)
        {
            // Arrange
            var templatesQuery = Substitute.For<ITemplatesQuery>();
            templatesQuery.GetTemplatesAsync(model).ReturnsForAnyArgs(Task.FromResult(content));
            TemplatesExternalController controller = new TemplatesExternalController(Substitute.For<ITemplatesCommand>(), templatesQuery);
            controller.SetUserForController(TestUserName, Claims);

            // Act
            IHttpActionResult result = await controller.GetPullRequestTemplate();

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Theory]
        [MemberData("DataForGetPullRuqestNotFoundResult")]
        public async void ShoudReturnNotFoundForGetPullRequestTemplate(GetTemplateModel model, string content)
        {
            // Arrange
            var templatesQuery = Substitute.For<ITemplatesQuery>();
            templatesQuery.GetTemplatesAsync(model).ReturnsForAnyArgs(content);
            TemplatesExternalController controller = new TemplatesExternalController(Substitute.For<ITemplatesCommand>(), templatesQuery);
            controller.SetUserForController(TestUserName, Claims);

            // Act
            IHttpActionResult result = await controller.GetPullRequestTemplate();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [MemberData("DataForIssueCreatedResult")]
        public async void ShoudReturnCreatedForCreateIssueTemplate(CreateUpdateTemplateModel model)
        {
            // Arrange
            var templatesCommand = Substitute.For<ITemplatesCommand>();
            templatesCommand.CreateTemplateAsync(model).ReturnsForAnyArgs(Task.FromResult(HttpStatusCode.Created));
            TemplatesExternalController controller = new TemplatesExternalController(templatesCommand, Substitute.For<ITemplatesQuery>());
            controller.SetUserForController(TestUserName, Claims);
            var expectedResult = HttpStatusCode.Created;

            // Act
            IHttpActionResult result = await controller.CreateIssueTemplate(model);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData("DataForIssueUnprocessableEntityResult")]
        public async void ShoudReturnUnprocessableEntityForCreateIssueTemplate(CreateUpdateTemplateModel model)
        {
            // Arrange
            var templatesCommand = Substitute.For<ITemplatesCommand>();
            templatesCommand.CreateTemplateAsync(model).ReturnsForAnyArgs(Task.FromResult((HttpStatusCode)422));
            TemplatesExternalController controller = new TemplatesExternalController(templatesCommand, Substitute.For<ITemplatesQuery>());
            controller.SetUserForController(TestUserName, Claims);
            var expectedResult = (HttpStatusCode)422;

            // Act
            IHttpActionResult result = await controller.CreateIssueTemplate(model);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData("DataForPullRequestCreatedResult")]
        public async void ShoudReturnCreatedForCreatePullRequestTemplate(CreateUpdateTemplateModel model)
        {
            // Arrange
            var templatesCommand = Substitute.For<ITemplatesCommand>();
            templatesCommand.CreateTemplateAsync(model).ReturnsForAnyArgs(Task.FromResult(HttpStatusCode.Created));
            TemplatesExternalController controller = new TemplatesExternalController(templatesCommand, Substitute.For<ITemplatesQuery>());
            controller.SetUserForController(TestUserName, Claims);
            var expectedResult = HttpStatusCode.Created;

            // Act
            IHttpActionResult result = await controller.CreatePullRequestTemplate(model);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData("DataForPullRequestUnprocessableEntityResult")]
        public async void ShoudReturnUnprocessableEntityForCreatePullRequestTemplate(CreateUpdateTemplateModel model)
        {
            // Arrange
            var templatesCommand = Substitute.For<ITemplatesCommand>();
            templatesCommand.CreateTemplateAsync(model).ReturnsForAnyArgs(Task.FromResult((HttpStatusCode)422));
            TemplatesExternalController controller = new TemplatesExternalController(templatesCommand, Substitute.For<ITemplatesQuery>());
            controller.SetUserForController(TestUserName, Claims);
            var expectedResult = (HttpStatusCode)422;

            // Act
            IHttpActionResult result = await controller.CreatePullRequestTemplate(model);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData("DataForIssueOkResult")]
        public async void ShoudReturnOkForIssueTemplate(CreateUpdateTemplateModel model)
        {
            // Arrange
            var templatesCommand = Substitute.For<ITemplatesCommand>();
            templatesCommand.UpdateTemplateAsync(model).ReturnsForAnyArgs(Task.FromResult(HttpStatusCode.OK));
            TemplatesExternalController controller = new TemplatesExternalController(templatesCommand, Substitute.For<ITemplatesQuery>());
            controller.SetUserForController(TestUserName, Claims);
            var expectedResult = HttpStatusCode.OK;

            // Act
            IHttpActionResult result = await controller.UpdateIssueTemplate(model);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData("DataForIssueUnprocessableEntityResult")]
        public async void ShoudReturnUnprocessableEntityForIssueTemplate(CreateUpdateTemplateModel model)
        {
            // Arrange
            var templatesCommand = Substitute.For<ITemplatesCommand>();
            templatesCommand.UpdateTemplateAsync(model).ReturnsForAnyArgs(Task.FromResult((HttpStatusCode)422));
            TemplatesExternalController controller = new TemplatesExternalController(templatesCommand, Substitute.For<ITemplatesQuery>());
            controller.SetUserForController(TestUserName, Claims);
            var expectedResult = (HttpStatusCode)422;

            // Act
            IHttpActionResult result = await controller.UpdateIssueTemplate(model);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData("DataForPullRequestOkResult")]
        public async void ShoudReturnOkForPullRequestTemplate(CreateUpdateTemplateModel model)
        {
            // Arrange
            var templatesCommand = Substitute.For<ITemplatesCommand>();
            templatesCommand.UpdateTemplateAsync(model).ReturnsForAnyArgs(Task.FromResult(HttpStatusCode.OK));
            TemplatesExternalController controller = new TemplatesExternalController(templatesCommand, Substitute.For<ITemplatesQuery>());
            controller.SetUserForController(TestUserName, Claims);
            var expectedResult = HttpStatusCode.OK;

            // Act
            IHttpActionResult result = await controller.UpdatePullRequestTemplate(model);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData("DataForPullRequestUnprocessableEntityResult")]
        public async void ShoudReturnUnprocessableEntityForPullRequestTemplate(CreateUpdateTemplateModel model)
        {
            // Arrange
            var templatesCommand = Substitute.For<ITemplatesCommand>();
            templatesCommand.UpdateTemplateAsync(model).ReturnsForAnyArgs(Task.FromResult((HttpStatusCode)422));
            TemplatesExternalController controller = new TemplatesExternalController(templatesCommand, Substitute.For<ITemplatesQuery>());
            controller.SetUserForController(TestUserName, Claims);
            var expectedResult = (HttpStatusCode)422;

            // Act
            IHttpActionResult result = await controller.UpdatePullRequestTemplate(model);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(expectedResult);
        }
    }
}
