using GitHubExtension.Activity.Internal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Activity.Internal.WebApi.Services.Interfaces
{
    public interface IActivityReaderService
    {
        ICollection<ActivityEvent> GetCurrentRepositoryUserActivities(int currenRepsitoryId, string userId);
        ICollection<ActivityEvent> GetUserActivities(string userId);
    }
}
