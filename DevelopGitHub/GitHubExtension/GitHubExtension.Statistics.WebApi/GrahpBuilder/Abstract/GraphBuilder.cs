using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Models;

namespace GitHubExtension.Statistics.WebApi.GrahpBuilder.Abstract
{
    public abstract class GraphBuilder
    {
        public Graph Graph { get; private set; }

        public GraphBuilder()
        {
            this.Graph = new Graph();
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
