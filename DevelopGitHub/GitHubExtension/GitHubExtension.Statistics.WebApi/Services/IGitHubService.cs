using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.Models;

namespace GitHubExtension.Statistics.WebApi.Services
{
    public interface IGitHubService
    {
        Task<GitHubUserModel> GetUserAsync(string token);
        Task<List<CommitDto>> GetCommitsForUser(string owner, string repository, string token);
    }
}
