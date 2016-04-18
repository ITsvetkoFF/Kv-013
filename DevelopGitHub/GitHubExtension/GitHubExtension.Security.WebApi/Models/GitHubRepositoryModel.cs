using System.ComponentModel.DataAnnotations.Schema;
using GitHubExtension.Infrastructure.Constants;
using Newtonsoft.Json;

namespace GitHubExtension.Security.WebApi.Models
{
    [NotMapped]
    public class GitHubRepositoryModel
    {
        [JsonProperty(PropertyName = GitHubConstants.Id)]
        public int GitHubId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.FullName)]
        public string FullName { get; set; }
    }
}