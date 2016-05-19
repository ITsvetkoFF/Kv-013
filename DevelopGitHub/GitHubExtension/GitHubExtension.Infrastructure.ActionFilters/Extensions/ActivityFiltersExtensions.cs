using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Infrastructure.ActionFilters.Constants;
using GitHubExtension.Infrastructure.ActionFilters.Models;
using GitHubExtension.Infrastructure.Extensions.Identity;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Infrastructure.ActionFilters.Extensions
{
    public static class ActivityFiltersExtensions
    {
        public static int GetRepositoryId(this HttpActionExecutedContext actionExecutedContext)
        {
            return (int)actionExecutedContext.ActionContext.ActionArguments[ActionFilterConstansts.RepositoryId];
        }

        public static string GetRoleToAssign(this HttpActionExecutedContext actionExecutedContext)
        {
            return (string)actionExecutedContext.ActionContext.ActionArguments[ActionFilterConstansts.RoleToAssign];
        }

        public static User GetUserByGitHubId(this HttpActionExecutedContext actionExecutedContext)
        {
            int gitHubId = actionExecutedContext.GetGitHubId();
            var applicationUserManager = actionExecutedContext.GetApplicationUserManager();
            User appUser = applicationUserManager.FindByGitHubId(gitHubId);

            return appUser;
        }

        public static UserModel GetUserModel(this HttpActionExecutedContext actionExecutedContext)
        {
            string userId = actionExecutedContext.GetUserId();
            string userName = actionExecutedContext.GetUserName();

            string imageUrl = actionExecutedContext.GetUserImageUrl(userId);


            return new UserModel() { UserId = userId, UserName = userName, ImageUrl = imageUrl };
        }

        public static RepositoryModel GetRepositoryModel(this HttpActionExecutedContext actionExecutedContext)
        {
            int repositoryId = actionExecutedContext.GetCurrentRepositoryId();
            string repositoryName = actionExecutedContext.GetCurrentRepositoryName();

            return new RepositoryModel() { Id = repositoryId, Name = repositoryName };
        }

        public static IActivityContextQuery GetIActivityContextQuery(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.GetDependencyResolver().GetService<IActivityContextQuery>();
        }

        public static IActivityContextCommand GetIActivityContextCommand(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.GetDependencyResolver().GetService<IActivityContextCommand>();
        }

        private static string GetUserId(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.ActionContext.RequestContext.Principal.Identity.GetUserId();
        }

        private static string GetUserName(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.ActionContext.RequestContext.Principal.Identity.Name;
        }

        private static int GetCurrentRepositoryId(this HttpActionExecutedContext actionExecutedContext)
        {
            return int.Parse(actionExecutedContext.ActionContext.RequestContext.Principal.GetCurrentProjectId());
        }

        private static string GetUserImageUrl(this HttpActionExecutedContext actionExecutedContext, string userId)
        {
            return actionExecutedContext.GetApplicationUserManager().FindById(userId).AvatarUrl;
        }

        private static string GetCurrentRepositoryName(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.ActionContext.RequestContext.Principal.GetCurrentProjectName();
        }

        private static IDependencyResolver GetDependencyResolver(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.ActionContext.RequestContext.Configuration.DependencyResolver;
        }

        private static T GetService<T>(this IDependencyResolver dependencyResolver) where T : class
        {
            return dependencyResolver.GetService(typeof(T)) as T;
        }

        private static ApplicationUserManager GetApplicationUserManager(this HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.GetDependencyResolver().GetService<ApplicationUserManager>();
        }

        private static int GetGitHubId(this HttpActionExecutedContext actionExecutedContext)
        {
            return (int)actionExecutedContext.ActionContext.ActionArguments[ActionFilterConstansts.GitHubId];
        }
    }
}
