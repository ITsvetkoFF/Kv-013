using GitHubExtension.Activity.Internal.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Commands
{
    public interface IContextActivityCommand
    {
        void AddActivity(ActivityEvent activityEvent);
    }
}
 