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
        public const string HeaderForShouldNotReturnNullWhenLinkHeaderIsNotNull =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=10>; rel=\"last\"";

        public const string HeaderForShouldNotThrowLinkHeaderMissingException =
           "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=10>; rel=\"last\"";

        public const string HeaderForShouldNotThrowLinkHeaderFormatException =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=10>; rel=\"last\"";

        public const string HeaderForShouldThrowLinkHeaderFormatException =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=10> rel=\"last\"";

        public const string HeaderForLastRelNotPresent =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\"";

        public const string HeaderForShouldNotReturnNullWhenLastRelIsPresent =
           "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=10>; rel=\"last\"";

        public const string HeaderForShouldThrowLinkHeaderFormatExceptionWhenLastRelPageIsNotNumber =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=q10>; rel=\"last\"";

        public const string HeaderForShouldParseLastRelHeaderCorrectly =
            "\"<https://api.github.com/repositories/52432402/events?page=2>; rel=\"next\", <https://api.github.com/repositories/52432402/events?page=2>; rel=\"last\"";
            
        [Fact]
        public void ShouldReturnNullWhenLinkHeaderIsNull()
        {
            // Arrange
            IEnumerable<string> requestLinkHeader = null;
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Act
            int? numberOfPages = query.GetNumberOfPages(requestLinkHeader);
        
            // Assert
            numberOfPages.Should().NotHaveValue("link header can not be present if we are on a last page or there is only one page");
        }

        [Fact]
        public void ShouldNotReturnNullWhenLinkHeaderIsNotNull()
        {
            // Arrange
            IEnumerable<string> requestLinkHeader = new List<string>() { HeaderForShouldNotReturnNullWhenLinkHeaderIsNotNull };
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Act
            int? numberOfPages = query.GetNumberOfPages(requestLinkHeader);

            // Assert
            numberOfPages.Should().HaveValue();
        }

        [Fact]
        public void ShouldThrowLinkHeaderMissingExceptionWhenItIsNotPresent()
        {
            // Arrange
            IEnumerable<string> requestLinkHeader = new List<string>();
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Assert
            query.Invoking(q => q.GetNumberOfPages(requestLinkHeader))
                .ShouldThrow<LinkHeaderMissingException>();
        }

        [Fact]
        public void ShouldNotThrowLinkHeaderMissingExceptionWhenLinkHeaderIsPresent()
        {
            // Arrange
            IEnumerable<string> requestLinkHeader = new List<string>() { HeaderForShouldNotThrowLinkHeaderMissingException };
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Assert
            query.Invoking(q => q.GetNumberOfPages(requestLinkHeader))
                .ShouldNotThrow<LinkHeaderMissingException>();
        }

        [Fact]
        public void ShouldReturnNullWhenLastRelIsNotPresent()
        {
            // Arrange
            IEnumerable<string> requestLinkHeader = new List<string>() { HeaderForLastRelNotPresent };
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Act
            int? page = query.GetNumberOfPages(requestLinkHeader);

            // Assert
            page.Should().NotHaveValue();
        }

        [Fact]
        public void ShouldNotReturnNullWhenLastRelIsPresent()
        {
            // Arrange
            IEnumerable<string> requestLinkHeader = new List<string>() { HeaderForShouldNotReturnNullWhenLastRelIsPresent };
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Act
            int? page = query.GetNumberOfPages(requestLinkHeader);

            // Assert
            page.Should().HaveValue();
        }

        [Fact]
        public void ShouldThrowLinkHeaderFormatExceptionWhenLinkHeaderNotValid()
        {
            // Arrange
            IEnumerable<string> requestLinkHeader = new List<string>() { HeaderForShouldThrowLinkHeaderFormatException };
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Assert
            query.Invoking(q => q.GetNumberOfPages(requestLinkHeader))
                .ShouldThrow<LinkHeaderFormatException>();
        }

        [Fact]
        public void ShouldNotThrowLinkHeaderFormatExceptionWhenLinkHeaderValid()
        {
            // Arrange
            IEnumerable<string> requestLinkHeader = new List<string>() { HeaderForShouldNotThrowLinkHeaderFormatException };
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Assert
            query.Invoking(q => q.GetNumberOfPages(requestLinkHeader))
                .ShouldNotThrow<LinkHeaderFormatException>();
        }

        [Fact]
        public void ShouldThrowLinkHeaderFormatExceptionWhenLastRelPageIsNotNumber()
        {
            // Arrange
            IEnumerable<string> requestLinkHeader = new List<string>() { HeaderForShouldThrowLinkHeaderFormatExceptionWhenLastRelPageIsNotNumber };
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Assert
            query.Invoking(q => q.GetNumberOfPages(requestLinkHeader))
                .ShouldThrow<LinkHeaderFormatException>();
        }

        [Fact]
        public void ShouldParseLastRelHeaderCorrectly()
        {
            int expectedValue = 2;
            // Arrange
            IEnumerable<string> requestLinkHeader = new List<string>() { HeaderForShouldParseLastRelHeaderCorrectly };
            IGitHubEventsQuery query = Substitute.For<IGitHubEventsQuery>();

            // Act
            int? page = query.GetNumberOfPages(requestLinkHeader);
            
            // Assert
            page.Should().HaveValue();
            page.ShouldBeEquivalentTo(expectedValue);
        }
    }
}
