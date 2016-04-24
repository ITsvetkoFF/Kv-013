using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.CommunicationModels
{
    public class RepositoryModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}