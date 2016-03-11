using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Services
{
    public interface IGithubService
    {
        Task<UserDto> GetUserAsync(string token);
        Task<List<RepositoryDto>> GetReposAsync(string userName, string token);
        Task<string> GetPrimaryEmailForUser(string token);
        Task<List<CollaboratorDto>> GetCollaboratorsForRepo(string owner, string repository, string token);
    }
}