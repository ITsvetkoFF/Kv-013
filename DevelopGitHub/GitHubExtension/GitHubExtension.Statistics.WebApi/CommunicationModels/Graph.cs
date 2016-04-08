using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.Models;

namespace GitHubExtension.Statistics.WebApi.CommunicationModels
{
    public class Graph
    {
        public Graph()
        {
            this.UserInfo = new UserInfo();
            this.Commits = new List<int>();
            this.Months = new List<string>();
            this.CommitsForEverRepository = new List<ICollection<int>>();
            this.Repositories = new List<Repository>();
        }

        public UserInfo UserInfo { get; set; }
        public virtual List<int> Commits { get; set; }
        public virtual ICollection<string> Months { get; set; }
        public virtual ICollection<ICollection<int>> CommitsForEverRepository { get; set; }
        public virtual ICollection<Repository> Repositories { get; set; }
    }
}
