﻿using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using GitHubExtension.Infrastructure.ActionFilters.Constants;
using GitHubExtension.Infrastructure.ActionFilters.Models;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Infrastructure.ActionFilters.Extensions
{
    public static class ActivityFiltersExtensions
    {
        private const string CurrentProjectName = "CurrentProjectName";
        private const string CurrentProjectId = "CurrentProjectId";

        public static string GetCurrentProjectName(this IPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirstValue(CurrentProjectName);
        }

        public static int GetCurrentProjectId(this IPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return Int32.Parse(claimsIdentity.FindFirstValue(CurrentProjectId));
        }

        public static IDependencyResolver GetDependencyResolver(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.ActionContext.RequestContext.Configuration.DependencyResolver;
        }

        public static string GetUserId(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.ActionContext.RequestContext.Principal.Identity.GetUserId();
        }

        public static string GetUserName(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.ActionContext.RequestContext.Principal.Identity.Name;
        }

        public static int GetCurrentRepositoryId(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.ActionContext.RequestContext.Principal.GetCurrentProjectId();
        }

        public static string GetCurrentRepositoryName(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.ActionContext.RequestContext.Principal.GetCurrentProjectName();
        }

        public static int GetRepositoryId(this HttpActionExecutedContext actionExecutedContext)
        {
            return (int) actionExecutedContext.ActionContext.ActionArguments[ActionFilterConstansts.RepositoryId];
        }

        public static string GetRoleToAssign(this HttpActionExecutedContext actionExecutedContext)
        {
            return (string) actionExecutedContext.ActionContext.ActionArguments[ActionFilterConstansts.RoleToAssign];
        }

        public static int GetGitHubId(this HttpActionExecutedContext actionExecutedContext)
        {
            return (int) actionExecutedContext.ActionContext.ActionArguments[ActionFilterConstansts.GitHubId];
        }

        public static T GetService<T>(this IDependencyResolver dependencyResolver) where T : class 
        {
            return dependencyResolver.GetService(typeof(T)) as T;
        }

        public static UserModel GetUserModel(this HttpActionExecutedContext actionExecutedContext)
        {
            string userId = actionExecutedContext.GetUserId();
            string userName = actionExecutedContext.GetUserName();

            return new UserModel() {UserId = userId, UserName = userName};
        }

        public static RepositoryModel GetRepositoryModel(this HttpActionExecutedContext actionExecutedContext)
        {
            int repositoryId = actionExecutedContext.GetCurrentRepositoryId();
            string repositoryName = actionExecutedContext.GetCurrentRepositoryName();

            return new RepositoryModel() { Id = repositoryId, Name = repositoryName};
        }
    }
}
