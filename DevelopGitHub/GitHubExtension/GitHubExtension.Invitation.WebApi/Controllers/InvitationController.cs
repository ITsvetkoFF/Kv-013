using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Infrastructure.SendEmail.Services;
using GitHubExtension.Invitation.WebApi.Constants;
using GitHubExtension.Invitation.WebApi.Mappers;
using GitHubExtension.Invitation.WebApi.ViewModels;

namespace GitHubExtension.Invitation.WebApi.Controllers
{
    public class InvitationController : ApiController
    {
        private IEmailSender _emailSender;

        public InvitationController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [Route(InvitationRouteConstants.ApiInvitation)]
        [HttpPost]
        public async Task<IHttpActionResult> SendInvitation(SendEmailRequestModel emailModel)
        {
            var invitationModel = emailModel.ToSendInvitationModel();

            try
            {
                await _emailSender.SendEmail(invitationModel.ToEmail, invitationModel.Subject, invitationModel.Body);

                return Ok();
            }
            catch (SmtpException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}