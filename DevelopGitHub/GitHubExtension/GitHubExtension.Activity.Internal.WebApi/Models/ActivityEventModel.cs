using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitHubExtension.Activity.Internal.WebApi.Models
{
    [NotMapped]
    public class ActivityEventModel
    {
        public int ActivityTypeId { get; set; }

        public int CurrentRepositoryId { get; set; }

        public DateTime? InvokeTime { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }
    }
}