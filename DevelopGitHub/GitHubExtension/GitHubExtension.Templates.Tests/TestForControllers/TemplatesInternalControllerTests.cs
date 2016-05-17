using System.Collections.Generic;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.Controllers;
using GitHubExtension.Templates.Queries;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Templates.Tests.TestForControllers
{
    public class TemplatesInternalControllerTests
    {
        private const string TestContent = "content";
        private const string TestDescription = "description";
        private const string TestIssueType = "Issue";
        private const string TestPrType = "PullRequest";
        private const int ValidCategoryId = 3;

        public static IEnumerable<object[]> DataForOkResultForPr
        {
            get
            {
                yield return new object[] 
                {
                    new List<TemplatesModel>
                    {
                        new TemplatesModel
                        {
                            CategoryId = 1,
                            Content = TestContent,
                            Id = 1,
                            TemplateDescription = TestDescription,
                            TemplateType = TestPrType
                        },
                        new TemplatesModel
                        {
                            CategoryId = 2,
                            Content = TestContent,
                            Id = 2,
                            TemplateDescription = TestDescription,
                            TemplateType = TestPrType
                        }
                    }
                };
            }
        }

        public static IEnumerable<object[]> DataForOkResultForIssue
        {
            get
            {
                yield return new object[] 
                {
                    new List<TemplatesModel>
                    {
                        new TemplatesModel
                        {
                            CategoryId = ValidCategoryId,
                            Content = TestContent,
                            Id = 1,
                            TemplateDescription = TestDescription,
                            TemplateType = TestIssueType
                        },
                        new TemplatesModel
                        {
                            CategoryId = ValidCategoryId,
                            Content = TestContent,
                            Id = 2,
                            TemplateDescription = TestDescription,
                            TemplateType = TestIssueType
                        }
                    },
                    ValidCategoryId
                };
            }
        }

        public static IEnumerable<object[]> DataForOkResultForCategories
        {
            get
            {
                yield return new object[] 
                {
                    new List<IssueCategoriesModel>
                    {
                        new IssueCategoriesModel
                        {
                            Id = 1,
                            CategoryDescription = TestDescription
                        },
                         new IssueCategoriesModel
                        {
                            Id = 2,
                            CategoryDescription = TestDescription
                        },
                         new IssueCategoriesModel
                        {
                            Id = 3,
                            CategoryDescription = TestDescription
                        }
                    }
                };
            }
        }

        [Theory]
        [MemberData("DataForOkResultForCategories")]
        public void ShouldReturnOkResultWhenCategoryExist(IEnumerable<IssueCategoriesModel> categories)
        {
            // Arrange
            var templatesQuery = Substitute.For<ITemplatesQuery>();
            templatesQuery.GetIssueTemplateCategories().Returns(categories);
            var templatesController = new TemplatesInternalController(templatesQuery);

            // Act
            var response = templatesController.GetIssueTemplateCategories();

            // Assert
            response.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<IssueCategoriesModel>>>();
        }

        [Theory]
        [MemberData("DataForOkResultForPr")]
        public void ShouldReturnOkResultWhenPullRequestExists(IEnumerable<TemplatesModel> templates)
        {
            // Arrange
            var templatesQuery = Substitute.For<ITemplatesQuery>();
            templatesQuery.GetPullRequests().Returns(templates);
            var templatesController = new TemplatesInternalController(templatesQuery);

            // Act
            var response = templatesController.GetPullRequests();

            // Assert
            response.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<TemplatesModel>>>();
        }

        [Theory]
        [MemberData("DataForOkResultForIssue")]
        public void ShouldReturnOkResultWhenIssueByCategoryIdExists(IEnumerable<TemplatesModel> templates, int id)
        {
            // Arrange
            var templatesQuery = Substitute.For<ITemplatesQuery>();
            templatesQuery.GetIssueTemplateByCategoryId(id).Returns(templates);
            var templatesController = new TemplatesInternalController(templatesQuery);

            // Act
            var response = templatesController.GetIssueTemplateByCategoryId(id);

            // Assert
            response.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<TemplatesModel>>>();
        }
    }
}
