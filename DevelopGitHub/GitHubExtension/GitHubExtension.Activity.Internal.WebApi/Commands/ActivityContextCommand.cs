using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Commands
{
    public class ActivityContextCommand : IActivityContextCommand
    {
        private readonly ActivityContext _activityContext;

        public ActivityContextCommand(ActivityContext activityContext)
        {
            _activityContext = activityContext;
        }

        public void AddActivity(ActivityEvent activityEvent)
        {
            _activityContext.Activities.Add(activityEvent);

            _activityContext.SaveChanges();
        }
    }
}