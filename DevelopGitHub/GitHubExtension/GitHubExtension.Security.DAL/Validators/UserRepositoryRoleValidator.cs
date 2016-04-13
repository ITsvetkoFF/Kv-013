using FluentValidation;
using GitHubExtension.Security.DAL.Identity;

namespace GitHubExtension.Security.DAL.Validators
{
    public class UserRepositoryRoleValidator : AbstractValidator<UserRepositoryRole>
    {
        public UserRepositoryRoleValidator()
        {
            RuleFor(r => r.RepositoryId).NotEmpty().GreaterThanOrEqualTo(ValidationConstants.MinValueForId);
            RuleFor(r => r.SecurityRoleId).NotEmpty().GreaterThanOrEqualTo(ValidationConstants.MinValueForId);
        }
    }
}
