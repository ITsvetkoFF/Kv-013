using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Services
{
    public interface IGithubService
    {
        Task<GitHubUserModel> GetUserAsync(string token);
        Task<List<GitHubRepositoryModel>> GetReposAsync(string token);
        Task<string> GetPrimaryEmailForUser(string token);
        Task<List<CollaboratorModel>> GetCollaboratorsForRepo(string owner, string repository, string token);
    }
}