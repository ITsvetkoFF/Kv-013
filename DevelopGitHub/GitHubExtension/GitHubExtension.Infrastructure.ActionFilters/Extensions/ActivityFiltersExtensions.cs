using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using GitHubExtension.Infrastructure.ActionFilters.Constants;
using GitHubExtension.Infrastructure.ActionFilters.Models;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Infrastructure.ActionFilters.Extensions
{
    public static class ActivityFiltersExtensions
    {
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
    }
}
