using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Infrastructure.Extensions;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.DAL.Model;
using GitHubExtension.Templates.ExtensionMethods;
using GitHubExtension.Templates.Mappers;

namespace GitHubExtension.Templates.Queries
{
    public class TemplatesQuery : ITemplatesQuery
    {
        private const string RouteToRepository = "https://api.github.com/repos/";
        private const string Contents = "/contents/";
        private const string AccessToken = "?access_token=";
        private readonly HttpClient _httpClient;
        private readonly TemplatesContext _templatesContext;

        public TemplatesQuery(TemplatesContext templatesContext)
        {
            _httpClient = new HttpClient();
            _templatesContext = templatesContext;
        }

        public async Task<string> GetTemplatesAsync(GetTemplateModel model)
        {
            var repositoryName = model.RepositoryName;
            var pathToFile = model.PathToFile;
            var token = model.Token;
            var pullTemplateUri = GetTemplatesRequestUri(repositoryName, pathToFile, token);
            var message = new HttpRequestMessage(HttpMethod.Get, pullTemplateUri).AddHeadersForGitHub();
            var response = await _httpClient.GetResponse(message);

            if (response.IsEmptyResponse())
            {
                return null;
            }

            var content = await response.GetTemplatesContent();

            return content;
        }

        public IEnumerable<TemplatesModel> GetPullRequests()
        {
            var pullRequestList = new List<TemplatesModel>();
            var pullRequests = _templatesContext.GetPr();
                 
            foreach (var pr in pullRequests)
            {
                pullRequestList.Add(pr.ToTemplateModel());
            }

            return pullRequestList;
        }

        public IEnumerable<IssueCategoriesModel> GetIssueTemplateCategories()
        {
            var listTemplatesCategories = new List<IssueCategoriesModel>();

            var issueTemplateCategories = _templatesContext.Set<Category>();

            foreach (var i in issueTemplateCategories)
            {
                listTemplatesCategories.Add(i.ToIssueCategoriesModel());
            }

            return listTemplatesCategories;        
        }

        public IEnumerable<TemplatesModel> GetIssueTemplateByCategoryId(int categoryId)
        {
            var listIssueByCategoryId = new List<TemplatesModel>();
            var issueTemplatesByCategories = _templatesContext.GetTemplates(categoryId);
            foreach (var issue in issueTemplatesByCategories)
            {
                listIssueByCategoryId.Add(issue.ToTemplateModel());
            }

            return listIssueByCategoryId;
        }

        private static string GetTemplatesRequestUri(string repositoryName, string pathToFile, string token)
        {
            var requestUri =
                string.Format(
                RouteToRepository + "{0}" + Contents + "{1}" + AccessToken + "{2}", 
                    repositoryName,
                    pathToFile,
                    token);
            return requestUri;
        }
    }
}