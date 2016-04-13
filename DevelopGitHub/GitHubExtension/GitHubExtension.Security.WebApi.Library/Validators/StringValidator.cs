using FluentValidation;

namespace GitHubExtension.Security.WebApi.Library.Validators
{
    class StringValidator : AbstractValidator<string>
    {
        public StringValidator()
        {
            RuleFor(c => c).NotEmpty();
        }
    }
}
