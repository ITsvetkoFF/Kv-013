using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GitHubExtension.Security.WebApi.Models
{
    [NotMapped]
    public class RepositoryDto
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int GitHubId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}