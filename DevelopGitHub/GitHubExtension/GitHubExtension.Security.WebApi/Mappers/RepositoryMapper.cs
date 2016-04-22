using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Mappers
{
    public static class RepositoryMapper
    {
        public static Repository ToEntity(this RepositoryViewModel repository)
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
    }

}
