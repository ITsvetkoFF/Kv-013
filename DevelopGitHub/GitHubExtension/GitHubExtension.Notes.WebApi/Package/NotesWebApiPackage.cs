using GitHubExtension.Notes.WebApi.Services;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Notes.WebApi.Package
{
    public class NotesWebApiPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<INotesService, NoteService>(Lifestyle.Scoped);
        }
    }
}