using System.Collections.Generic;
using System.Threading.Tasks;

using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Queries.Interfaces
{
    public interface IGitHubQuery
    {
        Task<List<CollaboratorModel>> GetCollaboratorsForRepo(string owner, string repository, string token);

        Task<string> GetPrimaryEmailForUser(string token);

        Task<List<RepositoryViewModel>> GetReposAsync(string token);

        Task<GitHubUserModel> GetUserAsync(string token);
    }
}