using System;
using System.Linq;
using FluentValidation;
using GitHubExtension.Security.WebApi.Library.Services;
using GitHubExtension.Security.WebApi.Library.Validators;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Security.WebApi.Library.Package
{
    public class WebApiLibraryPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IGithubService, GithubService>(Lifestyle.Singleton);
            container.Register<IAuthService, AuthService>(Lifestyle.Scoped);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            container.Register(typeof (IValidator<>), assemblies, Lifestyle.Singleton);
            container.RegisterConditional(typeof (IValidator<>), typeof (NullValidator<>), Lifestyle.Singleton,
                c => !c.Handled);
            container.Verify();
        }
    }
}
