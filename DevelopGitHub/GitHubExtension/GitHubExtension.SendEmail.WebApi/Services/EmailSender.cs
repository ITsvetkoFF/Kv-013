﻿using System.Net.Mail;
using System.Threading.Tasks;
using GitHubExtension.SendEmail.WebApi.Extensions;

namespace GitHubExtension.SendEmail.WebApi.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmail(string email, string subject, string body)
        {
            using (var mailMessage = new MailMessage())
            {
                mailMessage.SetEmailMessage(email, subject, body);

                using (var smtpClient = new SmtpClient())
                {
                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                    catch (SmtpException e)
                    {
                        throw;
                    }
                }
            }
        }
    }
}