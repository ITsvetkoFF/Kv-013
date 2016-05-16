using System.Net.Http;
using System.Text.RegularExpressions;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.Constants;
using GitHubExtension.Templates.Controllers;
using GitHubExtension.Tests.Infrastructure;
using MvcRouteTester;
using Xunit;

namespace GitHubExtension.Templates.Tests.TestRoutes
{
    public class TemplatesRouteTests : TestsRoutesConfig
    {
        [Fact]
        public void GetPullRequestTemplate()
        {
            url = url + RouteTemplatesConstants.PullRequestTemplate;
            GetRoutes().ShouldMap(url).To<TemplatesExternalController>(HttpMethod.Get, x => x.GetPullRequestTemplate());
        }

        [Fact]
        public void GetIssueTemplate()
        {
            url = url + RouteTemplatesConstants.IssueTemplate;
            GetRoutes().ShouldMap(url).To<TemplatesExternalController>(HttpMethod.Get, x => x.GetIssueTemplate());
        }

        [Fact]
        public void CreatePullRequestTemplate()
        {
            url = url + RouteTemplatesConstants.PullRequestTemplate;
            GetRoutes().ShouldMap(url).To<TemplatesExternalController>(
                HttpMethod.Post,
                x => x.CreatePullRequestTemplate(new CreateUpdateTemplateModel()));
        }

        [Fact]
        public void CreateIssueTemplate()
        {
            url = url + RouteTemplatesConstants.IssueTemplate;
            GetRoutes().ShouldMap(url).To<TemplatesExternalController>(
                HttpMethod.Post,
                x => x.CreateIssueTemplate(new CreateUpdateTemplateModel()));
        }

        [Fact]
        public void UpdatePullRequestTemplate()
        {
            url = url + RouteTemplatesConstants.PullRequestTemplate;
            GetRoutes().ShouldMap(url).To<TemplatesExternalController>(
                HttpMethod.Put,
                x => x.UpdatePullRequestTemplate(new CreateUpdateTemplateModel()));
        }

        [Fact]
        public void UpdateIssueTemplate()
        {
            url = url + RouteTemplatesConstants.IssueTemplate;
            GetRoutes().ShouldMap(url).To<TemplatesExternalController>(
                HttpMethod.Put,
                x => x.UpdateIssueTemplate(new CreateUpdateTemplateModel()));
        }

        [Fact]
        public void GetPullRequests()
        {
            url = url + RouteTemplatesConstants.GetPullRequests;
            GetRoutes().ShouldMap(url).To<TemplatesInternalController>(HttpMethod.Get, x => x.GetPullRequests());
        }

        [Fact]
        public void GetIssueTemplateCategories()
        {
            url = url + RouteTemplatesConstants.GetIssueTemplateCategories;
            GetRoutes().ShouldMap(url).To<TemplatesInternalController>(HttpMethod.Get, x => x.GetIssueTemplateCategories());
        }

        [Fact]
        public void GetIssueTemplateByCategoryId()
        {
            url = url + Regex.Replace(RouteTemplatesConstants.GetIssueTemplateByCategoryId, RouteTemplatesConstants.CategoryId, "/1");
            GetRoutes().ShouldMap(url).To<TemplatesInternalController>(HttpMethod.Get, x => x.GetIssueTemplateByCategoryId(1));
        }
    }
}