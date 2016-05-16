namespace GitHubExtension.Notes.WebApi.ViewModels
{
    public class NoteModel
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public string CollaboratorId { get; set; }

        public string UserId { get; set; }
    }
}