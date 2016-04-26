using System.ComponentModel.DataAnnotations.Schema;

namespace GitHubExtension.Security.WebApi.Models
{
    [NotMapped]
    public class CollaboratorModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Url { get; set; }
    }
}