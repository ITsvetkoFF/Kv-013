﻿using GitHubExtension.Constant;
using GitHubExtension.Security.WebApi.Library.Controllers;
using MvcRouteTester;
using System.Net.Http;
using Xunit;
namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class RolesTestRoutes : TestRoutesConfig
    {
        public RolesTestRoutes()
            : base(RouteConstants.ApiRoles)
        {

        }

        [Fact]
        public void RolesGetTest()
        {
            config.ShouldMap(url)
                .To<RolesController>(HttpMethod.Get,
                x => x.GetAllRoles());
        }
    }
}
