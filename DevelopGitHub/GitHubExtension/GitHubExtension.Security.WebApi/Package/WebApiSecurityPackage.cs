﻿using System;
using System.Linq;
using FluentValidation;
using GitHubExtension.Security.WebApi.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Security.WebApi.Package
{
    public class WebApiSecurityPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IGithubService, GithubService>(Lifestyle.Singleton);
            container.Register<IAuthService, AuthService>(Lifestyle.Scoped);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            container.Register(typeof (IValidator<>), assemblies, Lifestyle.Singleton);
        }
    }
}

