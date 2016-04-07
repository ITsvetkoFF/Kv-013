using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Models.CommunicationModels;

namespace GitHubExtension.Security.WebApi.Library.Services
{
    public interface IGithubService
    {
        Task<GitHubUserModel> GetUserAsync(string token);
        Task<List<RepositoryModel>> GetReposAsync(string token);
        Task<string> GetPrimaryEmailForUser(string token);
        Task<List<CollaboratorDto>> GetCollaboratorsForRepo(string owner, string repository, string token);
    }
}