using System.ComponentModel.DataAnnotations.Schema;

namespace GitHubExtension.Security.WebApi.Models
{
    [NotMapped]
    public class RepositoryViewModel
    {
        public string FullName { get; set; }

        public int GitHubId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}