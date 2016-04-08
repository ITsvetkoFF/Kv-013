using System.Threading.Tasks;

namespace GitHubExtension.Templates.WebApi.Services
{
        public interface ITemplateService
        {
            Task<string> GetPullRequestTemplatesAsync(string userName, string repositoryName, string pathToFile);
            Task<string> GetIssueTemplateAsync(string userName, string repositoryName, string pathToFile);
        }
}
