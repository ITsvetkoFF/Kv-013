using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Models
{
    public class CommitterDto
    {
        [JsonProperty(PropertyName = "author")]
        public GitHubUserModel Author { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
