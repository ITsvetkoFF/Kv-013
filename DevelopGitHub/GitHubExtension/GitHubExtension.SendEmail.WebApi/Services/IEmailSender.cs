using System.Threading.Tasks;

namespace GitHubExtension.SendEmail.WebApi.Services
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string body);
    }
}