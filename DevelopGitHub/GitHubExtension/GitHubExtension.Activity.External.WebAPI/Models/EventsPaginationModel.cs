using System.Collections.Generic;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class EventsPaginationModel
    {
        public int? AmountOfPages { get; set; }

        public IEnumerable<GitHubEventModel> Events { get; set; }
    }
}