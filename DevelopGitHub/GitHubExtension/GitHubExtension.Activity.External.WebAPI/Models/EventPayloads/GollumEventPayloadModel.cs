using System.Collections.Generic;

namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class GollumEventPayloadModel : PayloadModel
    {
        public List<PageModel> Pages { get; set; }
    }
}
