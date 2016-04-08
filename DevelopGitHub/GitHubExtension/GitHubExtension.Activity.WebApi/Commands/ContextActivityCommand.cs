using GitHubExtension.Activity.Internal.DAL;

namespace GitHubExtension.Activity.Internal.WebApi.Commands
{
    public class ContextActivityCommand : IContextActivityCommand
    {
        private readonly ActivityContext _activityContext;

        public ContextActivityCommand(ActivityContext activityContext)
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
