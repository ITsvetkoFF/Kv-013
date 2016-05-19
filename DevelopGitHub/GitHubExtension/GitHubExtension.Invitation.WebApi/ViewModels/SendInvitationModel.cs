namespace GitHubExtension.Invitation.WebApi.ViewModels
{
    public class SendInvitationModel
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}