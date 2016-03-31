using System;
using FluentValidation;
using SimpleInjector;

namespace GitHubExtension.Security.WebApi.Library.Validators.ValidatorFactories
{
    public class FluentValidatorFactory : ValidatorFactoryBase
    {
        private readonly Container _container;

        public FluentValidatorFactory(Container container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return ((IServiceProvider)_container).GetService(validatorType) as IValidator;
        }
    }
}
