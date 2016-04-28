using System.Data.Entity;
using System.Linq;
using GitHubExtension.Templates.DAL.Model;

namespace GitHubExtension.Templates.ExtensionMethods
{
    public static class TemplatesContextExtensions
    {
        private const string PullRequest = "PullRequest";

        public static IQueryable<Template> GetPr(this TemplatesContext context)
        {
            return context.Templates.Where(x => x.Type == PullRequest).AsNoTracking();
        }

        public static IQueryable<Template> GetTemplates(this TemplatesContext context, int categoryId)
        {
            return context.Templates.Where(x => x.CategoryId == categoryId).AsNoTracking();
        } 
    }
}