using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.CommunicationModels
{
    public class CommitsFromRepositoryModel
    {
        [JsonProperty(PropertyName = "all")]
        public List<int> Alls { get; set; }
        
        [JsonProperty(PropertyName = "owner")]
        public List<int> CommitsOwner { get; set; } 
    }
}
