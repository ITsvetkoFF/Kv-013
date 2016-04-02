using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Models.CommunicationModels
{
    public class ActivityEventViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CurrentProjectId { get; set; }
        public int ActivityTypeId { get; set; }
        public Nullable<DateTime> InvokeTime { get; set; }
    }
}
