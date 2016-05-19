using System.Net.Mail;

namespace GitHubExtension.Infrastructure.SendEmail.Extensions
{
    public static class MailMessageExtensions
    {
        public static void SetEmailMessage(this MailMessage mailMessage, string email, string subject, string body)
        {
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(email);
        }
    }
}