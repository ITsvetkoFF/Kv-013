using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Models
{
    public class Repository
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
