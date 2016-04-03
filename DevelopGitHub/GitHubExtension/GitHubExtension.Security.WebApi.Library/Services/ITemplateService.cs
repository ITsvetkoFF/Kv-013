using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Security.WebApi.Library.Services
{
    public interface ITemplateService
    {
        Task<string> GetPullRequestTemplatesAsync(string userName, string repositoryName, string pathToFile);
        Task<string> GetIssueTemplateAsync(string userName, string repositoryName, string pathToFile);
    }
}
