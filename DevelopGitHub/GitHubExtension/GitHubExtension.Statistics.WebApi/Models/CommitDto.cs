using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Models
{
    public class CommitDto
    {
        [JsonProperty(PropertyName = "commit")]
        public CommitterDto Commit { get; set; }

        [JsonProperty(PropertyName = "html_url")]
        public string Url { get; set; }
    }
}
