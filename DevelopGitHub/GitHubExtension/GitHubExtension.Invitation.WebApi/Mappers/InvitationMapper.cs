using GitHubExtension.Invitation.WebApi.ViewModels;

namespace GitHubExtension.Invitation.WebApi.Mappers
{
    public static class InvitationMapper
    {
        public static SendInvitationModel ToSendInvitationModel(SendEmailRequestModel emailRequestModel, string subject, string body)
        {
            var invitationModel = new SendInvitationModel
            {
                ToEmail = emailRequestModel.ToEmail,
                Subject = subject,
                Body = body
            };

            return invitationModel;
        }
    }
}