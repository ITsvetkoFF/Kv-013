using FluentValidation;
using GitHubExtension.Security.WebApi.Library.Services;
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
            container.Register(typeof (IValidator<>), new[] {typeof (IValidator<>).Assembly}, Lifestyle.Singleton);
            container.Verify();
        }
    }
}
