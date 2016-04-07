using FluentValidation;
using GitHubExtension.Security.StorageModels.Identity;

namespace GitHubExtension.Security.StorageModels.Validators
{
    public class RepositoryValidator : AbstractValidator<Repository>
    {
        public RepositoryValidator()
        {
            RuleFor(r => r.Id).NotEmpty().GreaterThanOrEqualTo(ValidationConstants.MinValueForId);
        }
    }
}
