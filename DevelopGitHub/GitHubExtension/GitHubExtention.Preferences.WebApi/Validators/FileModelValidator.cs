using FluentValidation;
using GitHubExtention.Preferences.WebApi.Models;

namespace GitHubExtention.Preferences.WebApi.Validators
{
    internal class FileModelValidator : AbstractValidator<FileModel>
    {
        public FileModelValidator()
        {
            RuleFor(m => m.Content).NotEmpty();
            RuleFor(m => m.Type).NotEmpty();
        }
    }
}
