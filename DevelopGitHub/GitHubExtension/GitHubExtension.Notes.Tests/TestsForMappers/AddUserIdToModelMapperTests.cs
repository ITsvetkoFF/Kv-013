using System.Collections.Generic;
using FluentAssertions;
using GitHubExtension.Notes.WebApi.Mappers;
using GitHubExtension.Notes.WebApi.ViewModels;
using Xunit;

namespace GitHubExtension.Notes.Tests.TestsForMappers
{
    public class AddUserIdToModelMapperTests
    {
        private const string TestNoteBody = "Note Body";
        private const string TestUserId = "550e8400-e29b-41d4-a716-446655441111";
        private const string TestCollaboratorId = "550e8400-e29b-41d4-a716-446655440000";

        public static IEnumerable<object[]> DataForPositiveTestsAddUserIdToModel
        {
            get
            {
                yield return new object[]
                {
                    new AddNoteModel
                    {
                        Body = TestNoteBody,
                        CollaboratorId = TestCollaboratorId
                    },
                    new NoteModel
                    {
                        Body = TestNoteBody,
                        CollaboratorId = TestCollaboratorId,
                        UserId = TestUserId
                    }
                };
            }
        }

        public static IEnumerable<object[]> DataForNegativeTestsAddUserIdToModel
        {
            get
            {
                yield return new object[]
                {
                    new List<AddNoteModel>
                    {
                        new AddNoteModel
                        {
                            Body = TestNoteBody,
                            CollaboratorId = TestCollaboratorId
                        },
                        new AddNoteModel
                        {
                            Body = TestNoteBody,
                        },
                        new AddNoteModel
                        {
                            CollaboratorId = TestCollaboratorId
                        }
                    },
                    new NoteModel
                    {
                        Body = TestNoteBody,
                        CollaboratorId = TestCollaboratorId,
                        UserId = TestUserId
                    }
                };
            }
        }

        [Theory]
        [MemberData("DataForPositiveTestsAddUserIdToModel")]
        public void AddUserIdToModelShouldReturnNoteModelWhenUserIdPassed(AddNoteModel addNoteModel,
            NoteModel expectedNoteModel)
        {
            // Act
            var noteModel = addNoteModel.AddUserIdToModel(TestUserId);

            // Assert
            noteModel.ShouldBeEquivalentTo(expectedNoteModel);
        }

        [Theory]
        [MemberData("DataForNegativeTestsAddUserIdToModel")]
        public void AddUserIdToModelFailsToReturnNoteModelWhenNoUserIdPassed(List<AddNoteModel> addNoteModel,
            NoteModel expectedNoteModel)
        {
            // Act
            var noteModel = addNoteModel[0].AddUserIdToModel(null);

            // Assert
            noteModel.UserId.Should().NotBe(expectedNoteModel.UserId);
        }

        [Theory]
        [MemberData("DataForNegativeTestsAddUserIdToModel")]
        public void AddUserIdToModelFailsToReturnNoteModelWhenDoesNotContainCollaboratorId(
            List<AddNoteModel> addNoteModel, NoteModel expectedNoteModel)
        {
            // Act
            var noteModel = addNoteModel[1].AddUserIdToModel(TestUserId);

            // Assert
            noteModel.CollaboratorId.Should().NotBe(expectedNoteModel.CollaboratorId);
        }

        [Theory]
        [MemberData("DataForNegativeTestsAddUserIdToModel")]
        public void AddUserIdToModelFailsToReturnNoteModelWhenDoesNotContainNoteBody(List<AddNoteModel> addNoteModel,
            NoteModel expectedNoteModel)
        {
            // Act
            var noteModel = addNoteModel[2].AddUserIdToModel(TestUserId);

            // Assert
            noteModel.Body.Should().NotBe(expectedNoteModel.Body);
        }
    }
}