using System.Configuration;
using GitHubExtension.Infrastructure.Constants;

namespace GitHubExtension.Invitation.WebApi.Constants
{
    public class InvitationDetailsConstants
    {
        public const string Subject = "Invitation to GitHubExtension";

        private static string body = "You have been invited to join <a href='"
            + ConfigurationManager.AppSettings[AppSettingConstants.Uri] + "'>GitHubExtension</a>";

        public static string Body
        {
            get { return body; }
        }
    }
}