using System.ComponentModel.DataAnnotations.Schema;

namespace GitHubExtension.Security.WebApi.Models
{
    [NotMapped]
    public class RepositoryViewModel
    {
        public int Id { get; set; }
        public int GitHubId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}