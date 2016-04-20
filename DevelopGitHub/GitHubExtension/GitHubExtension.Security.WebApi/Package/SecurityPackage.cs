using System;
using System.Linq;
using FluentValidation;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Queries.Implementations;
using GitHubExtension.Security.WebApi.Queries.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Security.WebApi.Package
{
    public class SecurityPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ISecurityContext>(() => new SecurityContext(), Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<SecurityRoleManager>(Lifestyle.Scoped);
            container.Register<IUserStore<User>, GitHubUserStore>(Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>, GitHubRoleStore>(Lifestyle.Scoped);
            container.Register<IGitHubQuery, GitHubQuery>(Lifestyle.Singleton);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            container.Register(typeof (IValidator<>), assemblies, Lifestyle.Singleton);
        }
    }
}

