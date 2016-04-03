using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Models
{
    public class CommitsFromRepository
    {
        [JsonProperty(PropertyName = "all")]
        public List<int> Alls { get; set; }
        
        [JsonProperty(PropertyName = "owner")]
        public List<string> CommitsOwner { get; set; } 
    }
}
