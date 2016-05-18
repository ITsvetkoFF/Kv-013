namespace GitHubExtension.SendEmail.WebApi.ViewModels
{
    public class SendEmailRequestModel
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }
        
        public string Body { get; set; }
    }
}