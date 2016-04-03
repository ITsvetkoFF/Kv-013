using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Statistics.WebApi.CommunicationModels
{
    public class Graph
    {
        public Graph()
        {
            this.Commits = new List<int>();
            this.Months = new List<string>();
        }
        public List<int> Commits { get; set; }
        public List<string> Months { get; set; }
    }
}
