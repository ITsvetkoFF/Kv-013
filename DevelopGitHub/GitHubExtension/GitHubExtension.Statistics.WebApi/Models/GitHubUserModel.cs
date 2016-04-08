using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Models
{
    public class GitHubUserModel
    {
        public string Login { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int GitHubId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
    }
}
