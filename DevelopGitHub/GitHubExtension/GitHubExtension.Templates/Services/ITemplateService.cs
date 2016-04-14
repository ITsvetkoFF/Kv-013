using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubExtension.Templates.Services
{
        public interface ITemplateService
        {
            Task<HttpResponseMessage> GetPullRequestTemplatesAsync(string userName, string repositoryName, string pathToFile);
            Task<HttpResponseMessage> GetIssueTemplateAsync(string userName, string repositoryName, string pathToFile);
        }
}
