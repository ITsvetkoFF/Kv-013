using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Mappers
{
    public static class RepositoryMapper
    {
        public static Repository ToEntity(this RepositoryDto repository)
        {
            var repositoryEntity = new Repository()
            {
                Id = repository.Id,
                GitHubId = repository.GitHubId,
                Name = repository.Name,
                Url = repository.Url
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
                Url = repository.Url
            };
        }
    }
}
