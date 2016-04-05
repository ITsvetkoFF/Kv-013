using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Activity.Internal.WebApi.Models
{
    [NotMapped]
    public class ActivityEventViewModel
    {
        public string UserId { get; set; }
        public int CurrentRepositoryId { get; set; }
        public int ActivityTypeId { get; set; }
        public Nullable<DateTime> InvokeTime { get; set; }
        public string Message { get; set; }
    }
}
