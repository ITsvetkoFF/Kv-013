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
    public class NoteControllerCreateNoteTests
    {
        private const string TestNoteBody = "Note Body";
        private const string TestUserId = "550e8400-e29b-41d4-a716-446655441111";
        private const string TestUserName = "Test";
        private const string TestCollaboratorId = "550e8400-e29b-41d4-a716-446655440000";
        private const string TypeOfClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        
        public static IEnumerable<object[]> DataForAddNoteModel
        {
            get
            {
                yield return new object[]
                {
                    new List<AddNoteModel>
                    {
                        new AddNoteModel()
                        {
                            CollaboratorId = TestCollaboratorId,
                            Body = TestNoteBody
                        }
                    }
                };
            }
        }

        [Theory]
        [MemberData("DataForAddNoteModel")]
        public void ShouldReturnOkResponseWhenIdentityContainsUser(IEnumerable<AddNoteModel> model)
        {
            // Arrange
            var noteController = new NoteController(Substitute.For<INoteCommands>(), Substitute.For<INoteQueries>());
            noteController.SetUserForController(TestUserName, TypeOfClaim, TestUserId);
           
            // Act
            var response = noteController.CreateNote(model.FirstOrDefault());
            var result = response.Result;


            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<Note>>();
        }

        [Theory]
        [MemberData("DataForAddNoteModel")]
        public void ShouldReturnBadRequestWhenIdentityContainsNoUser(IEnumerable<AddNoteModel> model)
        {
            // Arrange
            var noteController = new NoteController(Substitute.For<INoteCommands>(), Substitute.For<INoteQueries>());

            // Act
            var response = noteController.CreateNote(model.FirstOrDefault());
            var result = response.Result;

            // Assert
            result.Should().BeOfType<InvalidModelStateResult>();
        }

        [Theory]
        [MemberData("DataForAddNoteModel")]
        public void NoteCommanShouldReceiveACallWhenIdentitySet(IEnumerable<AddNoteModel> model)
        {
            // Arrange
            var noteCommand = Substitute.For<INoteCommands>();
            var noteController = new NoteController(noteCommand, Substitute.For<INoteQueries>());
            noteController.SetUserForController(TestUserName, TypeOfClaim, TestUserId);

            // Act
            noteController.CreateNote(model.FirstOrDefault());

            // Assert
            noteCommand.ReceivedWithAnyArgs().AddNote(new Note{});
        }

        [Theory]
        [MemberData("DataForAddNoteModel")]
        public void NoteCommanShouldNotReceiveACallWhenNoUser(IEnumerable<AddNoteModel> model)
        {
            // Arrange
            var noteCommand = Substitute.For<INoteCommands>();
            var noteController = new NoteController(noteCommand, Substitute.For<INoteQueries>());

            // Act
            noteController.CreateNote(model.Single());

            // Assert
            noteCommand.DidNotReceiveWithAnyArgs().AddNote(new Note{});
        }
    }
}