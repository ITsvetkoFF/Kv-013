using FluentValidation;

namespace GitHubExtension.Security.WebApi.Library.Validators
{
    class Int32Validator : AbstractValidator<int>
    {
        public Int32Validator()
        {
            RuleFor(value => value).NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
