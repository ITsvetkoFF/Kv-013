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
            url = url.ForRepositoryGetById();

            config.ShouldMap(url).To<RepositoryController>(HttpMethod.Get, x => x.GetById(13));
        }

        [Fact]
        public void RepositoryGetCollaboratorsForRepoTest()
        {
            url = url.ForRepositoryGetCollaboratorsForRepo();

            config.ShouldMap(url)
                .To<RepositoryController>(HttpMethod.Get, x => x.GetCollaboratorsForRepo("myRepository"));
        }

        [Fact]
        public void RepositoryGetReposForCurrentUserTest()
        {
            url = url.ForRepositoryGetReposForCurrentUser();

            config.ShouldMap(url).To<RepositoryController>(HttpMethod.Get, x => x.GetRepositoryForCurrentUser());
        }
    }
}