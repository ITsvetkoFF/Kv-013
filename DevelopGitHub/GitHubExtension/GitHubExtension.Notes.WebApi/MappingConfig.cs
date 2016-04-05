using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.WebApi.ViewModels;

namespace GitHubExtension.Notes.WebApi
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Note, AddNoteModel>()
                    .ReverseMap();
            });
        }
    }
}