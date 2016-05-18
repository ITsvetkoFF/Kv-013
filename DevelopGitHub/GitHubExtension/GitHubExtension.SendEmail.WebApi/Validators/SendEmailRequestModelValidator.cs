using FluentValidation;

using GitHubExtension.SendEmail.WebApi.ViewModels;

namespace GitHubExtension.SendEmail.WebApi.Validators
{
    public class SendEmailRequestModelValidator : AbstractValidator<SendEmailRequestModel>
    {
        public SendEmailRequestModelValidator()
        {
            RuleFor(m => m.ToEmail).NotEmpty();
            RuleFor(m => m.ToEmail).EmailAddress();
            RuleFor(m => m.Subject).NotEmpty();
            RuleFor(m => m.Body).NotEmpty();
        }
    }
}