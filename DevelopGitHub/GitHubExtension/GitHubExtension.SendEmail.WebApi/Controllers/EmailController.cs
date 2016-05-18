using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.SendEmail.WebApi.Constants;
using GitHubExtension.SendEmail.WebApi.Services;
using GitHubExtension.SendEmail.WebApi.ViewModels;

namespace GitHubExtension.SendEmail.WebApi.Controllers
{
    public class EmailController : ApiController
    {
        private IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [Route(EmailRouteConstants.ApiEmail)]
        [HttpPost]
        public async Task<IHttpActionResult> SendEmail(SendEmailRequestModel emailModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _emailSender.SendEmail(emailModel.ToEmail, emailModel.Subject, emailModel.Body);

                return Ok();
            }
            catch (SmtpException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}