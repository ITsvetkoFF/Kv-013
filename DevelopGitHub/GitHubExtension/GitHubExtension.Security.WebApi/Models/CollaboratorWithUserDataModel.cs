namespace GitHubExtension.Security.WebApi.Models
{
    public class CollaboratorWithUserDataModel
    {
        public string UserId { get; set; }

        public int GitHubId { get; set; }

        public string Login { get; set; }

        public string Url { get; set; }

        public string Mail { get; set; }
    }
}