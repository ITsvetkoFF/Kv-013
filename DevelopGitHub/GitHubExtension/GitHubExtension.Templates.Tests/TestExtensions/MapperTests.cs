using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentAssertions;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.DAL.Model;
using GitHubExtension.Templates.Mappers;
using Xunit;

namespace GitHubExtension.Templates.Tests.TestExtensions
{
    public class GitHubTemplatesMappersTests
    {
        private const string TestDescription = "description";
        private const int TestId = 1;
        private const string TestContent = "content";
        private const string TestTemplateType = "Issue";

        public static IEnumerable<object[]> DataForIssueCategoriesModelTest
        {
            get
            {
                yield return
                    new object[]
                    {
                        new Category
                        {
                            Id = TestId,
                            Description = TestDescription
                        },
                        new IssueCategoriesModel
                        {
                            Id = TestId,
                            CategoryDescription = TestDescription
                        }
                    };
            }
        }

        public static IEnumerable<object[]> DataForTemplatesModelTest
        {
            get
            {
                yield return
                    new object[]
                    {
                        new Template
                        {
                            Id = TestId,
                            Content = TestContent,
                            Type = TestTemplateType,
                            CategoryId = TestId,
                            Description = TestDescription
                        },
                        new TemplatesModel
                        {
                             Id = TestId,
                             Content = TestContent,
                             TemplateType = TestTemplateType,
                             CategoryId = TestId,
                             TemplateDescription = TestDescription
                        }
                    };
            }
        }

        [Theory]
        [MemberData("DataForTemplatesModelTest")]
        public void TemplatesModelTest(Template entityToTemplateModel, TemplatesModel expectedTemplatesModelModel)
        {
            // Act
            TemplatesModel templatesModel = entityToTemplateModel.ToTemplateModel();

            // Assert
            templatesModel.ShouldBeEquivalentTo(expectedTemplatesModelModel);
        }

        [Theory]
        [MemberData("DataForIssueCategoriesModelTest")]
        public void IssueCategoriesModelTest(Category entityToIssueCategoriesModel, IssueCategoriesModel expectedIssueCategoriesModel)
        {
            // Act
            IssueCategoriesModel categoriesModel = entityToIssueCategoriesModel.ToIssueCategoriesModel();

            // Assert
            categoriesModel.ShouldBeEquivalentTo(expectedIssueCategoriesModel);
        }
    }
}
