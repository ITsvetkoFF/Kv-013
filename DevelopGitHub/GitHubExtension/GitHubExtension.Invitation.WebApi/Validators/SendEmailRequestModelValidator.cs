using FluentValidation;
using GitHubExtension.Invitation.WebApi.ViewModels;

namespace GitHubExtension.Invitation.WebApi.Validators
{
    public class SendEmailRequestModelValidator : AbstractValidator<SendEmailRequestModel>
    {
        public SendEmailRequestModelValidator()
        {
            RuleFor(m => m.ToEmail).NotEmpty();
            RuleFor(m => m.ToEmail).EmailAddress();
        }
    }
}