﻿using GitHubExtension.Constant;
using GitHubExtension.Security.WebApi.Library.Controllers;
using MvcRouteTester;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xunit;
namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class RepositoryTestRoutes : TestRoutesConfig
    {
        [Fact]
        public void RepositoryGetByIdTest()
        {
            url = "/" + Regex.Replace(
                RouteConstant.getByIdRepository,
                RouteConstant.id_int,
                "13");

            config.ShouldMap(url)
                .To<RepositoryController>(HttpMethod.Get,
                x => x.GetById(13));
        }

        [Fact]
        public void RepositoryGetReposForCurrentUserTest()
        {
            url = "/" + RouteConstant.getRepositoryForCurrentUser;

            config.ShouldMap(url)
                .To<RepositoryController>(HttpMethod.Get,
                x => x.GetRepositoryForCurrentUser());
        }

        [Fact]
        public void RepositoryGetCollaboratorsForRepoTest()
        {
            url = "/" + Regex.Replace(
                RouteConstant.getCollaboratorsForRepository,
                RouteConstant.repositoryName,
                "myRepository");

            config.ShouldMap(url)
                .To<RepositoryController>(HttpMethod.Get,
                x => x.GetCollaboratorsForRepo("myRepository"));
        }
    }
}
