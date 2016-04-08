using FluentValidation;
using FluentValidation.Results;

namespace GitHubExtension.Security.WebApi.Library.Validators
{
    sealed class NullValidator<T> : AbstractValidator<T>
    {
        public ValidationResult Validation(T instance)
        {
            return Validate(instance);
        }
    }
}

