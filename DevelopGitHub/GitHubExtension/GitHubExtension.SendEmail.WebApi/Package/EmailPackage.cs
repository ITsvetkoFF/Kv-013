using GitHubExtension.SendEmail.WebApi.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.SendEmail.WebApi.Package
{
    public class EmailPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IEmailSender, EmailSender>(Lifestyle.Scoped);
        }
    }
}