using GitHubExtension.Infrastructure.Constants;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class PageModel
    {
        [JsonProperty(PropertyName = GitHubConstants.PageName)]
        public string PageName { get; set; }
        public string Title { get; set; }
        public object Summary { get; set; }
        public string Action { get; set; }
        public string Sha { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
    }
}