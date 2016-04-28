namespace GitHubExtension.Templates.CommunicationModels
{
    public class AddFileToGitHubModel
    {
        public string Message { get; set; }
        public CommiterModel Committer { get; set; }
        public string Content { get; set; }
    }
}