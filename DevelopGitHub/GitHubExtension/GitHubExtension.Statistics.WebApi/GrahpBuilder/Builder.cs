using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.GrahpBuilder.Abstract;
using GitHubExtension.Statistics.WebApi.Models;

namespace GitHubExtension.Statistics.WebApi.GrahpBuilder
{
    public class Builder
    {
        public async Task Build(
            GraphBuilder grahpBuilder,
            string userName,string token,
            DateTime from, DateTime to)
        {
            await grahpBuilder.SetRepositories(userName, token);
            grahpBuilder.SetRepositoryCount();
            grahpBuilder.SetFollowerCount(userName, token);
            grahpBuilder.SetFollowingCount(userName, token);
            await grahpBuilder.SetAllCommitsForUser(userName, token);
            grahpBuilder.SetGroupCommits();
            grahpBuilder.SetMonths(from, to);
        }
    }
}
