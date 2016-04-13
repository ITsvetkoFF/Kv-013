using GitHubExtension.Notes.DAL.Model;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtension.Notes.DAL.Package
{
    public class NotesDALPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<NoteContext>(Lifestyle.Scoped);
        }
    }
}