using GitHubExtension.Notes.WebApi.Commands;
using GitHubExtension.Notes.WebApi.Queries;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Notes.WebApi.Package
{
    public class NotesWebApiPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<INoteCommands, NoteCommands>(Lifestyle.Scoped);
            container.Register<INoteQueries, NoteQueries>(Lifestyle.Scoped);
        }
    }
}