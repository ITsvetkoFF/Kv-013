using System;
using GitHubExtension.Activity.DAL;

namespace GitHubExtension.Infrastructure.ActionFilters.InternalActivitiesFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UserJoinToSystemActivityAttribute : InternalActivityFilter
    {
        public override string ActivityTypeName
        {
            get { return ActivityTypeNames.JoinToSystem; }
        }
    }
}
