using FluentValidation;
using GitHubExtension.Activity.Internal.WebApi.Models;

namespace GitHubExtension.Activity.Internal.WebApi.Validators
{
    public class RoleActivityModelValidator : AbstractValidator<RoleActivityModel>
    {
        public RoleActivityModelValidator()
        {
            RuleFor(r => r.RoleToAssign).NotEmpty();
        }
    }
}
