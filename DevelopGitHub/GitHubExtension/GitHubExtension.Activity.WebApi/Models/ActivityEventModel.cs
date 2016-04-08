using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitHubExtension.Activity.Internal.WebApi.Models
{
    [NotMapped]
    public class ActivityEventModel
    {
        public string UserId { get; set; }
        public int CurrentRepositoryId { get; set; }
        public int ActivityTypeId { get; set; }
        public DateTime? InvokeTime { get; set; }
        public string Message { get; set; }
    }
}
