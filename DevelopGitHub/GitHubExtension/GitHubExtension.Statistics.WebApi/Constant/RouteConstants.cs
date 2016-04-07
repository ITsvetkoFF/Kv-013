using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Statistics.WebApi.Constant
{
    public class StatisticsRouteConstants
    {
        public const string User = "api/user";
        public const string GetUserCommits = User+"/"+"commits";
        public const string GetRepoByName = GetUserCommits+"/"+"{name}";
    }
}
