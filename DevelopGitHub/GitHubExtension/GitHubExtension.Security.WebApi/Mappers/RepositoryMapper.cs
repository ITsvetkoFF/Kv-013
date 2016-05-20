using System.Linq;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Queries.Interfaces;

namespace GitHubExtension.Security.WebApi.Mappers
{
    public static class RepositoryMapper
    {
        public static Repository ToEntity(this GitHubRepositoryModel repository)
        {
            var repositoryEntity = new Repository()
            {
                GitHubId = repository.GitHubId, 
                Name = repository.Name, 
                Url = repository.Url, 
                FullName = repository.FullName
            };

            return repositoryEntity;
        }

        public static RepositoryViewModel ToRepositoryViewModel(this Repository repository)
        {
            return new RepositoryViewModel()
            {
                GitHubId = repository.GitHubId, 
                Id = repository.Id, 
                Name = repository.Name, 
                Url = repository.Url, 
                FullName = repository.FullName
            };
        }

        public static User GetUserByUserId(this ApplicationUserManager userManager, string userId)
        {
            return userManager.Users.FirstOrDefault(el => el.Id == userId);
        }

        public static string GetRole(this ISecurityContextQuery securityContextQuery, int securityRoleId)
        {
            return securityContextQuery.SecurityRoles.FirstOrDefault(el => el.Id == securityRoleId).Name;
        }

        public static int GetSecurityRoleId(this User user, string currentProgectId)
        {
            return user.UserRepositoryRoles.Where(el => el.RepositoryId.ToString() == currentProgectId)
                        .Select(el => el.SecurityRoleId).FirstOrDefault();
        }

        public static string GetCurrentProgectId(this User user)
        {
            return user.Claims.Where(el => el.ClaimType == "CurrentProjectId")
                .Select(el => el.ClaimValue).FirstOrDefault();
        }
    }
}