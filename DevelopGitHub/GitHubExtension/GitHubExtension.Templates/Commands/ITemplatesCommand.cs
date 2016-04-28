using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Templates.CommunicationModels;

namespace GitHubExtension.Templates.Commands
{
    public interface ITemplatesCommand
    {
        Task<HttpStatusCode> CreateTemplateAsync(CreateUpdateTemplateModel model);

        Task<HttpStatusCode> UpdateTemplateAsync(CreateUpdateTemplateModel model);

        Task<string> GetShaTemplate(string repositoryName, string pathToFile, string token);
    }
}