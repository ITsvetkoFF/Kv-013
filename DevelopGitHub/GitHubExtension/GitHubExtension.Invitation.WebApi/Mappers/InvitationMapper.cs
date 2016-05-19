using GitHubExtension.Invitation.WebApi.Constants;
using GitHubExtension.Invitation.WebApi.ViewModels;

namespace GitHubExtension.Invitation.WebApi.Mappers
{
    public static class InvitationMapper
    {
        public static SendInvitationModel ToSendInvitationModel(this SendEmailRequestModel emailRequestModel)
        {
            var invitationModel = new SendInvitationModel
            {
                ToEmail = emailRequestModel.ToEmail,
                Subject = InvitationDetailsConstants.Subject,
                Body = InvitationDetailsConstants.Body
            };

            return invitationModel;
        }
    }
}