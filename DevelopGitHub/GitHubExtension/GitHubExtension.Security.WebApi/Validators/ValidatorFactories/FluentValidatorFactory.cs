using System;
using FluentValidation;
using SimpleInjector;

namespace GitHubExtension.Security.WebApi.Validators.ValidatorFactories
{
    public class FluentValidatorFactory : ValidatorFactoryBase
    {
        private readonly Container container;

        public FluentValidatorFactory(Container container)
        {
            this.container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return this.container.GetInstance(validatorType) as IValidator;
        }
    }
}
