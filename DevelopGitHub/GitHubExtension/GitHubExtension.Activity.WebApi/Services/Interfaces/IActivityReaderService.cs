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
        // get user activities for current repository
        ICollection<ActivityEvent> GetCurrentRepositoryUserActivities(int currenRepsitoryId);

        // get all user activities
        ICollection<ActivityEvent> GetUserActivities(string userId);
    }
}
