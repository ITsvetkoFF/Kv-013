using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Statistics.WebApi.CommunicationModels
{
    public class UserInfo
    {
        public int RepositoryCount { get; set; }
        public int FollowerCount { get; set; }
        public int FolowingCount { get; set; }
    }
}
