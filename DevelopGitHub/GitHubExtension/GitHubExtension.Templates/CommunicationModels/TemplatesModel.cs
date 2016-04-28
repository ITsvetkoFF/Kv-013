namespace GitHubExtension.Templates.CommunicationModels
{
    public class TemplatesModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string TemplateType { get; set; }

        public int? CategoryId { get; set; }

        public string TemplateDescription { get; set; }
    }
}
