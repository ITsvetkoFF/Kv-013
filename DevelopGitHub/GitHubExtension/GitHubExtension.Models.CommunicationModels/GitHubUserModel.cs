using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GitHubExtension.Models.CommunicationModels
{
    [NotMapped]
    public class GitHubUserModel
    {
        public string Login { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int GitHubId { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}