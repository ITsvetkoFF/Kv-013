using FluentValidation;
using GitHubExtension.Notes.WebApi.Constants;
using GitHubExtension.Notes.WebApi.ViewModels;

namespace GitHubExtension.Notes.WebApi.Validators
{
    public class AddNoteModelValidator : AbstractValidator<NoteModel>
    {
        public AddNoteModelValidator()
        {
            RuleFor(m => m.Body).NotEmpty().Length(ValidationConstants.MinBodyLength, ValidationConstants.MaxBodyLength);
        }
    }
}