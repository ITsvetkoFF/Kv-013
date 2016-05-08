using System.Collections.Generic;
using FluentAssertions;
using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.WebApi.Mappers;
using GitHubExtension.Notes.WebApi.ViewModels;
using Xunit;

namespace GitHubExtension.Notes.Tests.TestsForMappers
{
    public class NoteMapperTests
    {
        private const string TestNoteBody = "Note Body";
        private const string TestUserId = "550e8400-e29b-41d4-a716-446655441111";
        private const string TestCollaboratorId = "550e8400-e29b-41d4-a716-446655440000";

        public static IEnumerable<object[]> DataForPositiveTests
        {
            get
            {
                yield return new object[]
                {
                    new Note
                    {
                        Body = TestNoteBody,
                        CollaboratorId = TestCollaboratorId,
                        UserId = TestUserId
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

        public static IEnumerable<object[]> DataForNegativeTestsToNoteViewModel
        {
            get
            {
                yield return new object[]
                {
                    new List<Note> 
                    {
                        new Note
                        {
                            Body = TestNoteBody,
                            CollaboratorId = TestCollaboratorId,
                        },
                        new Note
                        {
                            Body = TestNoteBody,
                            UserId = TestUserId
                        },
                        new Note
                        {
                            CollaboratorId = TestCollaboratorId,
                            UserId = TestUserId
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

        public static IEnumerable<object[]> DataForNegativeTestsToEntity
        {
            get
            {
                yield return new object[]
                {
                    new Note
                    {
                        Body = TestNoteBody,
                        CollaboratorId = TestCollaboratorId,
                        UserId = TestUserId
                    },
                    new List<NoteModel> 
                    {
                        new NoteModel
                        {
                            Body = TestNoteBody,
                            CollaboratorId = TestCollaboratorId,
                        },
                        new NoteModel
                        {
                            Body = TestNoteBody,
                            UserId = TestUserId
                        },
                        new NoteModel
                        {
                            CollaboratorId = TestCollaboratorId,
                            UserId = TestUserId
                        }
                    },
                };
            }
        }

        [Theory]
        [MemberData("DataForPositiveTests")]
        public void ToEntityShouldReturnEntityWhenModelHasAllProperties(Note expectedNoteEntity, NoteModel noteModel)
        {
            // Act
            var noteEntity = noteModel.ToEntity();

            // Assert
            noteEntity.ShouldBeEquivalentTo(expectedNoteEntity);
        }

        [Theory]
        [MemberData("DataForPositiveTests")]
        public void ToNoteViewModelShouldReturnModelWhenEntityHasAllProperties(Note noteEntity,
            NoteModel expectedNoteModel)
        {
            // Act
            var noteModel = noteEntity.ToNoteViewModel();

            // Assert
            noteModel.ShouldBeEquivalentTo(expectedNoteModel);
        }

        [Theory]
        [MemberData("DataForNegativeTestsToNoteViewModel")]
        public void ToNoteViewModelFailsToReturnModelWhenEntityWithNoUserId(List<Note> noteEntity,
            NoteModel expectedNoteModel)
        {
            // Act
            var noteModel = noteEntity[0].ToNoteViewModel();

            // Assert
            noteModel.UserId.Should().NotBe(expectedNoteModel.UserId);
        }

        [Theory]
        [MemberData("DataForNegativeTestsToNoteViewModel")]
        public void ToNoteViewModelFailsToReturnModelWhenEntityWithNoCollaboratorId(List<Note> noteEntity,
            NoteModel expectedNoteModel)
        {
            // Act
            var noteModel = noteEntity[1].ToNoteViewModel();

            // Assert
            noteModel.CollaboratorId.Should().NotBe(expectedNoteModel.CollaboratorId);
        }

        [Theory]
        [MemberData("DataForNegativeTestsToNoteViewModel")]
        public void ToNoteViewModelFailsToReturnModelWhenEntityWithNoNoteBody(List<Note> noteEntity,
            NoteModel expectedNoteModel)
        {
            // Act
            var noteModel = noteEntity[2].ToNoteViewModel();

            // Assert
            noteModel.Body.Should().NotBe(expectedNoteModel.Body);
        }

        [Theory]
        [MemberData("DataForNegativeTestsToEntity")]
        public void ToEntityFailsToReturnEntityWhenModelWithNoUserId(Note expectedNoteEntity,
            List<NoteModel> noteModel)
        {
            // Act
            var noteEntity = noteModel[0].ToEntity();

            // Assert
            noteEntity.UserId.Should().NotBe(expectedNoteEntity.UserId);
        }

        [Theory]
        [MemberData("DataForNegativeTestsToEntity")]
        public void ToEntityFailsToReturnEntityWhenModelWithNoCollaboratorId(Note expectedNoteEntity,
            List<NoteModel> noteModel)
        {
            // Act
            var noteEntity = noteModel[1].ToEntity();

            // Assert
            noteEntity.CollaboratorId.Should().NotBe(expectedNoteEntity.CollaboratorId);
        }

        [Theory]
        [MemberData("DataForNegativeTestsToEntity")]
        public void ToEntityFailsToReturnEntityWhenModelWithNoNoteBody(Note expectedNoteEntity,
            List<NoteModel> noteModel)
        {
            // Act
            var noteEntity = noteModel[2].ToEntity();

            // Assert
            noteEntity.Body.Should().NotBe(expectedNoteEntity.Body);
        }
    }
}