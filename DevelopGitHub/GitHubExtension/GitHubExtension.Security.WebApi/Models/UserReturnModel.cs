namespace GitHubExtension.Security.WebApi.Models
{
    public class UserReturnModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public int GitHubId { get; set; }
    }
}
