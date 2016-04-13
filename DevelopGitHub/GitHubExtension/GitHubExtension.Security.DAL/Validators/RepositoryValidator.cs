using FluentValidation;
using GitHubExtension.Security.DAL.Identity;

namespace GitHubExtension.Security.DAL.Validators
{
    public class RepositoryValidator : AbstractValidator<Repository>
    {
        public RepositoryValidator()
        {
            RuleFor(r => r.Id).NotEmpty().GreaterThanOrEqualTo(ValidationConstants.MinValueForId);
        }
    }
}
