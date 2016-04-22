using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Queries.Interfaces
{
    public interface IGitHubQuery
    {
        Task<GitHubUserModel> GetUserAsync(string token);
        Task<string> GetPrimaryEmailForUser(string token);
        Task<List<CollaboratorModel>> GetCollaboratorsForRepo(string owner, string repository, string token);
        Task<List<RepositoryViewModel>> GetReposAsync(string token);
    }
}
