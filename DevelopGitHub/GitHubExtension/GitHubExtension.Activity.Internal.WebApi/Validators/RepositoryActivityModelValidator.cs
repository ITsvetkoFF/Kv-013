using FluentValidation;
using GitHubExtension.Activity.Internal.WebApi.Models;

namespace GitHubExtension.Activity.Internal.WebApi.Validators
{
    public class RepositoryActivityModelValidator : AbstractValidator<RepositoryActivityModel>
    {
        public RepositoryActivityModelValidator()
        {
            RuleFor(r => r.RepositoryId).NotEmpty();
        }
    }
}
