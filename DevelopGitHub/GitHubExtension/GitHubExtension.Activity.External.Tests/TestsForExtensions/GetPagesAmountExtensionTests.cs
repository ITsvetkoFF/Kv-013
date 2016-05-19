using System.Collections.Generic;
using FluentAssertions;
using GitHubExtension.Activity.External.WebAPI.Exceptions;
using GitHubExtension.Activity.External.WebAPI.Extensions;
using GitHubExtension.Activity.External.WebAPI.Queries;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Activity.External.Tests.TestsForExtensions
{
    public class GetPagesAmountExtensionTests
    {
        public const string HeaderForShouldThrowLinkHeaderFormatException =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=10> rel=\"last\"";

        public const string HeaderForLastRelNotPresent =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"prev\"";

        public const string HeaderForLastRelPageIsNotNumber =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=q10>; rel=\"last\"";

        public const string ValidLinkHeader =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=2>; rel=\"last\"";

        public static IEnumerable<object[]> DataForLinkHeaderMissingException
        {
            get
            {
                yield return new object[] {
                    new List<string>()
                };
            }
        }

        public static IEnumerable<object[]> DataForLinkHeaderFormatException
        {
            get
            {
                yield return new object[] {
                    new string[] { HeaderForShouldThrowLinkHeaderFormatException }
                };
            }
        }

        public static IEnumerable<object[]> DataForLinkHeaderPageNotAnumber
        {
            get
            {
                yield return new object[] {
                    new string[] { HeaderForLastRelPageIsNotNumber }
                };
            }
        }

        [Theory]
        [MemberData("DataForLinkHeaderMissingException")] 
        public void ShouldThrowLinkHeaderMissingExceptionWhenItIsNotPresent(IEnumerable<string> requestLinkHeader)
        {
            // Arrange
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Assert
            query.Invoking(q => q.GetNumberOfPages(requestLinkHeader))
                .ShouldThrow<LinkHeaderMissingException>();
        }

        [Theory]
        [MemberData("DataForLinkHeaderFormatException")]
        public void ShouldThrowLinkHeaderFormatExceptionWhenLinkHeaderNotValid(IEnumerable<string> requestLinkHeader)
        {
            // Arrange
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Assert
            query.Invoking(q => q.GetNumberOfPages(requestLinkHeader))
                .ShouldThrow<LinkHeaderFormatException>();
        }

        [Theory]
        [MemberData("DataForLinkHeaderPageNotAnumber")]
        public void ShouldThrowLinkHeaderFormatExceptionWhenLastRelPageIsNotNumber(IEnumerable<string> requestLinkHeader)
        {
            // Arrange
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Assert
            query.Invoking(q => q.GetNumberOfPages(requestLinkHeader))
                .ShouldThrow<LinkHeaderFormatException>();
        }

        [Theory]
        [InlineData(new [] { ValidLinkHeader }, 2)]
        [InlineData(null, null)]
        [InlineData(new [] { HeaderForLastRelNotPresent }, null)]
        public void ShouldParseLastRelHeaderCorrectly(IEnumerable<string> requestLinkHeader, int? expectedResult)
        {
            // Arrange
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Act
            int? page = query.GetNumberOfPages(requestLinkHeader);
            
            // Assert
            page.ShouldBeEquivalentTo(expectedResult);
        }
    }
}
