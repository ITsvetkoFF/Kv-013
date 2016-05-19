using System.Threading.Tasks;

namespace GitHubExtension.Infrastructure.SendEmail.Services
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string body);
    }
}