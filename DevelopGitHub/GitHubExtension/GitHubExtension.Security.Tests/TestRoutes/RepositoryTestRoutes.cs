using System.Net.Http;
using GitHubExtension.Security.Tests.TestRoutes.TestRoutesMappers;
using GitHubExtension.Security.WebApi.Controllers;
using MvcRouteTester;
using Xunit;

namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class RepositoryTestRoutes : TestRoutesConfig
    {
        public RepositoryTestRoutes()
            : base(null)
        {
        }

        [Fact]
        public void RepositoryGetByIdTest()
        {
            this.Url = this.Url.ForRepositoryGetById();

            this.Config.ShouldMap(this.Url).To<RepositoryController>(HttpMethod.Get, x => x.GetById(13));
        }

        [Fact]
        public void RepositoryGetReposForCurrentUserTest()
        {
            this.Url = this.Url.ForRepositoryGetReposForCurrentUser();

            this.Config.ShouldMap(this.Url).To<RepositoryController>(HttpMethod.Get, x => x.GetRepositoryForCurrentUser());
        }

        [Fact]
        public void RepositoryGetCollaboratorsForRepoTest()
        {
            this.Url = this.Url.ForRepositoryGetCollaboratorsForRepo();

            this.Config.ShouldMap(this.Url).To<RepositoryController>(HttpMethod.Get, x => x.GetCollaboratorsForRepo("myRepository"));
        }
    }
}
