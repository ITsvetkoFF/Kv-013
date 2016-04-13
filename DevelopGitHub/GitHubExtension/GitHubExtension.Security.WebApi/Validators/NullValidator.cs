using FluentValidation;
using FluentValidation.Results;

namespace GitHubExtension.Security.WebApi.Validators
{
    sealed class NullValidator<T> : AbstractValidator<T>
    {
        public ValidationResult Validation(T instance)
        {
            return Validate(instance);
        }
    }
}

