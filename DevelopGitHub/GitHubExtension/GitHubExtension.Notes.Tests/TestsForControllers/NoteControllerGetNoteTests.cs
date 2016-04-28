using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.Tests.Extensions;
using GitHubExtension.Notes.WebApi.Commands;
using GitHubExtension.Notes.WebApi.Controllers;
using GitHubExtension.Notes.WebApi.Queries;
using GitHubExtension.Notes.WebApi.ViewModels;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Notes.Tests.TestsForControllers
{
    public class NoteControllerGetNoteTests
    {
        private const string TestUserId = "550e8400-e29b-41d4-a716-446655441111";
        private const string TestUserName = "Test";
        private const string TestCollaboratorId = "550e8400-e29b-41d4-a716-446655440000";
        private const string TestNoteBody = "Test note body";
        private const string TypeOfClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public static IEnumerable<object[]> DataForNotFoundResult
        {
            get
            {
                yield return new object[] 
                {
                    new List<Note>
                    {
                        new Note
                        {
                            UserId = "testNotFound",
                            CollaboratorId = "testNotFound",
                            Body = null
                        }
                    }
                };
            }
        }

        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[]
                {
                    new List<Note>
                    {
                        new Note
                        {
                            UserId = TestUserId,
                            CollaboratorId = TestCollaboratorId,
                            Body = TestNoteBody
                        }
                    }
                };
            }
        }

        private static INoteQueries MockForNoteQueriesContext(IEnumerable<Note> notes)
        {
            var context = notes.AsQueryable().OrderBy(x => x.Id);
            var noteQueriesContext = Substitute.For<INoteQueries>();
            noteQueriesContext.Notes.Returns(context);
            return noteQueriesContext;
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void ShouldReturnOkResultWhenNoteExists(IEnumerable<Note> notes)
        {
            // Arrange
            var noteQuieriesContext = MockForNoteQueriesContext(notes);
            var noteController = new NoteController(Substitute.For<INoteCommands>(), noteQuieriesContext);
            noteController.SetUserForController(TestUserName, TypeOfClaim, TestUserId);

            // Act
            var response = noteController.GetNote(TestCollaboratorId);

            // Assert
            response.Should().BeOfType<OkNegotiatedContentResult<NoteModel>>();
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void ShouldReturnBadRequestWhenNoUser(IEnumerable<Note> notes)
        {
            // Arrange
            var noteQuieriesContext = MockForNoteQueriesContext(notes);
            var noteController = new NoteController(Substitute.For<INoteCommands>(), noteQuieriesContext);

            // Act
            var response = noteController.GetNote(TestCollaboratorId);

            // Assert
            response.Should().BeOfType<InvalidModelStateResult>();
        }

        [Theory]
        [MemberData("DataForNotFoundResult")]
        public void ShouldReturnNotFoundResultWhenNoteDoesNotExist(IEnumerable<Note> notes)
        {
            // Arrange
            var noteQuieriesContext = MockForNoteQueriesContext(notes);
            var noteController = new NoteController(Substitute.For<INoteCommands>(), noteQuieriesContext);
            noteController.SetUserForController(TestUserName, TypeOfClaim, TestUserId);

            // Act
            var response = noteController.GetNote(TestCollaboratorId);

            // Assert
            response.Should().BeOfType<NotFoundResult>();
        }
    }
}