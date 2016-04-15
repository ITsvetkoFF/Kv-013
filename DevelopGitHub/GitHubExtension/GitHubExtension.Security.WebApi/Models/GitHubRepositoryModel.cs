using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GitHubExtension.Security.WebApi.Models
{
    [NotMapped]
    public class GitHubRepositoryModel
    {
        [JsonProperty(PropertyName = "id")]
        public int GitHubId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }
    }
}