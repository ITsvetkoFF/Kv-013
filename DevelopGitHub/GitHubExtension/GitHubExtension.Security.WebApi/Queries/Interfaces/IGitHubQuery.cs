using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtention.Preferences.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Queries.Interfaces
{
    public interface IGitHubQuery
    {
        Task<List<CollaboratorModel>> GetCollaboratorsForRepo(string owner, string repository, string token);

        Task<string> GetPrimaryEmailForUser(string token);

        Task<List<GitHubRepositoryModel>> GetReposAsync(string token);

        Task<GitHubUserModel> GetUserAsync(string token);

        Task<FileModel> GetAvatar(string url);
    }
}