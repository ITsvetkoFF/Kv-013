using FluentValidation;

using GitHubExtension.Activity.Internal.WebApi.Models;

namespace GitHubExtension.Activity.Internal.WebApi.Validators
{
    public class ActivityEventModelValidator : AbstractValidator<ActivityEventModel>
    {
        public ActivityEventModelValidator()
        {
            RuleFor(r => r.UserId).NotEmpty();
        }
    }
}