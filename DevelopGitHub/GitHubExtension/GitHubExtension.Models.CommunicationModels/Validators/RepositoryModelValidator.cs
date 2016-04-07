using FluentValidation;

namespace GitHubExtension.Models.CommunicationModels.Validators
{
    public class RepositoryModelValidator : AbstractValidator<RepositoryModel>
    {
        public RepositoryModelValidator()
        {
            RuleFor(r => r.Id).NotEmpty().GreaterThanOrEqualTo(ValidationConstants.MinValueForId);
        }
    }
}
