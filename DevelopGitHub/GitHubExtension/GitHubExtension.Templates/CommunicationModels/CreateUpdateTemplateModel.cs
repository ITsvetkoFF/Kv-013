using GitHubExtension.Infrastructure.Constants;
using Newtonsoft.Json;

namespace GitHubExtension.Templates.CommunicationModels
{
    public class CreateUpdateTemplateModel
    {
        [JsonProperty(PropertyName = GitHubConstants.Message)]
        public string Message { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.Content)]
        public string Content { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.Sha)]
        public string Sha { get; set; }

        public string Token { get; set; }

        public string RepositoryName { get; set; }

        public string Path { get; set; }
    }
}
