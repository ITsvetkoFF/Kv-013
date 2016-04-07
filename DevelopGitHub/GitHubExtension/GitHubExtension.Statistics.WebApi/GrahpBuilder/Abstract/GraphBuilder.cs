using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;

namespace GitHubExtension.Statistics.WebApi.GrahpBuilder.Abstract
{
    public abstract class GraphBuilder
    {
        public GraphModel Graph { get; private set; }

        public GraphBuilder()
        {
            this.Graph = new GraphModel();
        }

        public abstract Task SetRepositories(string userName, string token);
        public abstract void SetRepositoryCount();
        public abstract Task SetFollowerCount(string owner, string token);
        public abstract Task SetFollowingCount(string userName, string token);
        public abstract Task SetAllCommitsForUser(string userName, string token);
        public abstract void SetGroupCommits();
        public abstract void SetMonths(DateTime from, DateTime to);
    }
}
