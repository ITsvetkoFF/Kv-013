using FluentValidation.TestHelper;
using GitHubExtension.Notes.WebApi.Validators;
using Xunit;

namespace GitHubExtension.Notes.Tests.TestsForValidator
{
    public class NoteValidatorTests
    {
        [Fact]
        public void ShoulHaveErrorWhenBodyIsNull()
        {
            var noteValidator = new AddNoteModelValidator();
            noteValidator.ShouldHaveValidationErrorFor(note => note.Body, null as string);
        }

        [Fact]
        public void ShoulHaveErrorWhenCollaboratorIdIsNull()
        {
            var noteValidator = new AddNoteModelValidator();
            noteValidator.ShouldHaveValidationErrorFor(note => note.CollaboratorId, null as string);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenBodyIsSpecified()
        {
            var noteValidator = new AddNoteModelValidator();
            noteValidator.ShouldNotHaveValidationErrorFor(note => note.Body, "Great Coder");
        }

        [Fact]
        public void ShouldNotHaveErrorWhenCollaboratorIdIsSpecified()
        {
            var noteValidator = new AddNoteModelValidator();
            noteValidator.ShouldNotHaveValidationErrorFor(note => note.Body, "550e8400-e29b-41d4-a716-446655440000");
        }
    }
}