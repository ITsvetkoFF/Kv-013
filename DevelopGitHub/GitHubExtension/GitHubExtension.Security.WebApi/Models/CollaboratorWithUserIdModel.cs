namespace GitHubExtension.Security.WebApi.Models
{
    public class CollaboratorWithUserIdModel
    {
        public string UserId { get; set; }

        public int GitHubId { get; set; }

        public string Login { get; set; }

        public string Url { get; set; }
    }
}