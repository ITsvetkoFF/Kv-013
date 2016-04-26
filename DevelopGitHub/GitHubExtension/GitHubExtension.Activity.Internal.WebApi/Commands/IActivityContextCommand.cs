using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Commands
{
    public interface IActivityContextCommand
    {
        void AddActivity(ActivityEvent activityEvent);
    }
}