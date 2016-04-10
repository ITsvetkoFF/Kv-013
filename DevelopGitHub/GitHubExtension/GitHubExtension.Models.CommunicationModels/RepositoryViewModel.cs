using System.ComponentModel.DataAnnotations.Schema;

namespace GitHubExtension.Models.CommunicationModels
{
    [NotMapped]
    public class RepositoryViewModel
    {
        public int Id { get; set; }
        public int GitHubId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}