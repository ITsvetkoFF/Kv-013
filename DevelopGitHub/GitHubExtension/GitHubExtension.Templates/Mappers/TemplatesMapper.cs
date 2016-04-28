using System.Runtime.CompilerServices;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.DAL.Model;

namespace GitHubExtension.Templates.Mappers
{
    public static class TemplatesMapper
    {
        public static IssueCategoriesModel ToIssueCategoriesModel(this Category categoryEntity)
        {
            var categoryModel = new IssueCategoriesModel
            {
                Id = categoryEntity.Id,
                CategoryDescription = categoryEntity.Description
            };
            return categoryModel;
        }

        public static TemplatesModel ToTemplateModel(this Template templateEntity)
        {
            var templatesModel = new TemplatesModel
            {
                Id = templateEntity.Id,
                Content = templateEntity.Content,
                TemplateType = templateEntity.Type,
                CategoryId = templateEntity.CategoryId,
                TemplateDescription = templateEntity.Description
            };
            return templatesModel;
        }

        public static CreateUpdateTemplateModel ToCreateModel(
            this CreateUpdateTemplateModel model,
            string path,
            string repositoryName,
            string token)
        {
            var templatesModel = new CreateUpdateTemplateModel
            {
                Content = model.Content,
                Message = model.Message,
                Path = path,
                RepositoryName = repositoryName,
                Token = token
            };
            return templatesModel;
        }

        public static CreateUpdateTemplateModel ToUpdateModel(
            this CreateUpdateTemplateModel model,
            string path,
            string repositoryName,
            string token,
            string sha)
        {
            var templatesModel = new CreateUpdateTemplateModel
            {
                Content = model.Content,
                Message = model.Message,
                Path = path,
                RepositoryName = repositoryName,
                Token = token,
                Sha = sha
            };
            return templatesModel;
        }

        public static GetTemplateModel ToGetTemplateModel(
            this string repositoryName,
            string pathToFile,
            string token)
        {
            var model = new GetTemplateModel()
            {
                PathToFile = pathToFile,
                RepositoryName = repositoryName,
                Token = token
            };
            return model;
        }
    }
}
