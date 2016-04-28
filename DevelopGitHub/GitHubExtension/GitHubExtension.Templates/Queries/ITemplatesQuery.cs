using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Templates.CommunicationModels;

namespace GitHubExtension.Templates.Queries
{
    public interface ITemplatesQuery
    {
        Task<string> GetTemplatesAsync(GetTemplateModel model);

        IEnumerable<TemplatesModel> GetPullRequests();

        IEnumerable<IssueCategoriesModel> GetIssueTemplateCategories();

        IEnumerable<TemplatesModel> GetIssueTemplateByCategoryId(int id);
    }
}