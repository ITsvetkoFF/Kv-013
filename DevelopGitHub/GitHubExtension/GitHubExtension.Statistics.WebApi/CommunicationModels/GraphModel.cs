using System.Collections.Generic;

namespace GitHubExtension.Statistics.WebApi.CommunicationModels
{
    public class GraphModel
    {
        public GraphModel()
        {
            this.UserInfo = new UserInfoModel();
            this.Commits = new List<int>();
            this.Months = new List<string>();
            this.CommitsForEverRepository = new List<ICollection<int>>();
            this.Repositories = new List<RepositoryModel>();
        }

        public UserInfoModel UserInfo { get; set; }
        public virtual List<int> Commits { get; set; }
        public virtual ICollection<string> Months { get; set; }
        public virtual ICollection<ICollection<int>> CommitsForEverRepository { get; set; }
        public virtual ICollection<RepositoryModel> Repositories { get; set; }
    }
}
