using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.BLL.Interfaces;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;

namespace GitHubExtension.Statistics.WebApi.Queries.Implementations
{
    public class StatisticsQuery : IStatisticsQuery
    {
        #region fields
        private readonly IGraphBll _graphQuery;
        #endregion

        public StatisticsQuery(IGraphBll graphQuery)
        {
            this._graphQuery = graphQuery;
        }

        public async Task<GraphModel> GraphCreation(string userName, string token)
        {
            ICollection<RepositoryModel> repositories = await _graphQuery.GetRepositories(userName, token);
            ICollection<ICollection<int>> commitsEverRepository =
                await _graphQuery.GetAllCommitsUser(userName, token, repositories);

            #region create graph
            GraphModel graph = new GraphModel()
            {
                UserInfo = new UserInfoModel()
                {
                    FollowerCount = await _graphQuery.GetFollowerCount(userName, token),
                    FolowingCount = await _graphQuery.GetFollowingCount(userName, token),
                    RepositoryCount = repositories.Count
                },

                Months = _graphQuery.GetMountsFromDateTo(),
                Repositories = repositories,
                CommitsForEverRepository = commitsEverRepository,
                Commits = _graphQuery.GetGroupCommits(commitsEverRepository)
            };
            #endregion

            return graph;
        }

        public async Task<List<int>> GetCommitsRepository(string userName, string token, string repository)
        {
            RepositoryModel repositoryModel = new RepositoryModel() { Name = repository };
            ICollection<ICollection<int>> col = await _graphQuery.GetAllCommitsUser(userName, token, new List<RepositoryModel>() {repositoryModel});
            return col.ToList().Select(comm => comm.ToList()).FirstOrDefault();
        }
    }
}
