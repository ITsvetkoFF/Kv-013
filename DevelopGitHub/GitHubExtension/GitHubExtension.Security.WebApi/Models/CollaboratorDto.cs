using System.ComponentModel.DataAnnotations.Schema;

namespace GitHubExtension.Security.WebApi.Models
{
    [NotMapped]
    public class CollaboratorDto
    {
        public int Id { get; set; }
        
        public string Url { get; set; }
        
        public string Login { get; set; }
    }
}