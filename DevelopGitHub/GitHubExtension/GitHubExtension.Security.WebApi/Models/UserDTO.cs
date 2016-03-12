using Newtonsoft.Json;
﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GitHubExtension.Security.WebApi.Models
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