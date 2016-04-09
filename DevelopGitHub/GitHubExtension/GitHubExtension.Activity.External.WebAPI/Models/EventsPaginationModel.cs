using System.Collections.Generic;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class EventsPaginationModel
    {
        public IEnumerable<GitHubEventModel> Events { get; set; }
        public int? AmountOfPages { get; set; }
    }
}
