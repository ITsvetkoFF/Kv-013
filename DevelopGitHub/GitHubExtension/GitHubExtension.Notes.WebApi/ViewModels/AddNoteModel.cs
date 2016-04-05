namespace GitHubExtension.Notes.WebApi.ViewModels
{
    public class AddNoteModel
    {
        public string UserId { get; set; }
        public string CollaboratorId { get; set; }
        public string Body { get; set; }
    }
}