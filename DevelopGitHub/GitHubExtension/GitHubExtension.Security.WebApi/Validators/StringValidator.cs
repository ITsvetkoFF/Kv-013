using FluentValidation;

namespace GitHubExtension.Security.WebApi.Validators
{
    class StringValidator : AbstractValidator<string>
    {
        public StringValidator()
        {
            RuleFor(c => c).NotEmpty();
        }
    }
}