using FluentValidation;
using GitHubExtension.Templates.CommunicationModels;

namespace GitHubExtension.Templates.Validators
{
    public class CreateTemplateModelValidator : AbstractValidator<CreateUpdateTemplateModel>
    {
        public CreateTemplateModelValidator()
        {
            RuleFor(m => m.Content).NotNull();
            RuleFor(m => m.Message).NotNull();
        }
    }
}