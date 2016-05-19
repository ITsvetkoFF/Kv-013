using GitHubExtension.Infrastructure.SendEmail.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Infrastructure.SendEmail.Package
{
    public class EmailPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IEmailSender, EmailSender>(Lifestyle.Scoped);
        }
    }
}